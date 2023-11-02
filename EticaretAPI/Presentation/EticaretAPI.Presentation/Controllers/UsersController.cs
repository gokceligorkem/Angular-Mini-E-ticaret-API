using EticaretAPI.Application.Abstraction.Services;
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
        //[HttpGet]
        //public  async Task<IActionResult> ExampleMail()
        //{
        //    await _mailservice.SendMailAsync("baranngunay@gmail.com", "Adammm", "<strong>Baran bey KVKK yı onaylıyor musunuz?</strong>");
        //    return Ok();
        //}

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordCommandRequest updatePassword)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePassword);
            return Ok(response);
        }
    }
}
