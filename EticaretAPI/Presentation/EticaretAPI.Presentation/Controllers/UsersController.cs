using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.FacebookLoginUserComamnd;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.GoogLoginUserCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.LoginUserCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        { 
           CreateUserCommandResponse response=await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
     

    }
}
