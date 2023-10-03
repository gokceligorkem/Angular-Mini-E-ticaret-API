using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.FacebookLoginUserComamnd;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.GoogLoginUserCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.LoginUserCommand;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.RefreshTokenCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommanRequest googleLoginCommanRequest)
        {
            GoogleLoginCommanResponse response = await _mediator.Send(googleLoginCommanRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody]RefreshTokenCommanRequest refreshTokenCommanRequest)
        {
            RefreshTokenCommanResponse response = await _mediator.Send(refreshTokenCommanRequest);
            return Ok(response);
        }
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {
            FacebookloginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }
    }
}
