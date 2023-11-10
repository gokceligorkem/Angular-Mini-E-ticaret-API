using Azure.Core;
using Azure;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoleById;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.CreateRoleCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.UpdateRoleCommand;
using static Google.Apis.Requests.BatchRequest;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.DeleteRoleCommand;
using EticaretAPI.Application.CustomAttribute;
using EticaretAPI.Application.Enums;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefition(ActionType =ActionType.Reading,Definition ="Get Roles",Menu ="Roles")]
        public async Task<IActionResult> GetRoles([FromQuery]GetRolesQueryRequest request)
        {
            GetRolesQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefition(ActionType = ActionType.Reading, Definition = "Get Role By Id", Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromRoute]GetRoleByIdQueryRequest request)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost()]
        [AuthorizeDefition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = "Roles")]

        public async Task<IActionResult> CreateRoles([FromBody]CreateRoleCommandRequest request)
        {
          CreateRoleCommandResponse  response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        [AuthorizeDefition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = "Roles")]

        public async Task<IActionResult> UpdateRoles([FromBody]UpdateRoleCommandRequest request)
        {
           UpdateRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = "Roles")]

        public async Task<IActionResult> DeleteRoles([FromRoute]DeleteRoleCommandRequest request)
        {
          DeleteRoleCommandResponse  response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
