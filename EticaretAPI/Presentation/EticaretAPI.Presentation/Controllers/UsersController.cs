using Azure.Core;
using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.CustomAttribute;
using EticaretAPI.Application.Enums;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.AssignRoleToUserCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.FacebookLoginUserComamnd;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.GoogLoginUserCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.LoginUserCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Query.AppUserGetAllQuery;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Query.GetRolesToUserQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService _mailservice;



        public UsersController(IMediator mediator, IMailService mailservice)
        {
            _mediator = mediator;
            _mailservice = mailservice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        { 
           CreateUserCommandResponse response=await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordCommandRequest updatePassword)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePassword);
            return Ok(response);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Admin")]
        [AuthorizeDefition(ActionType =ActionType.Writing,Menu ="Users",Definition ="Get All users")]
        public async Task<IActionResult> GetAllUsers([FromQuery]AppUserGetAllUserQueryRequest request)
        {
            AppUserGetAllUserQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(ActionType = ActionType.Writing, Menu = "Users", Definition = "Get roles to users")]
        public async Task<IActionResult> GetRolesToUsers([FromRoute]GetRolesToUserQueryRequest request)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefition(ActionType = ActionType.Reading, Menu = "Users", Definition = "Assign role to user")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest request)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
