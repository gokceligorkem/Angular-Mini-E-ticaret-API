using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.AuthorizationEndPointsCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.AuthorizationEndPointsQuery;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesEndPoint(GetRolesEndPointQueryRequest request  )
        {
            GetRolesEndPointQueryResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> AssignRole(AssignRoleEndPointCommandRequest request)
        {
            request.Type = typeof(Program);
         AssignRoleEndPointCommandResponse response=await _mediator.Send(request);
            return Ok(response);
        }
    }
}
