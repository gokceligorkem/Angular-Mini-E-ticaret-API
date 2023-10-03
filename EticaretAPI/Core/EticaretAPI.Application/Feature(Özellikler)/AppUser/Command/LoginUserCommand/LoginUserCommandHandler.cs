using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.DTOs;
using EticaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.LoginUserCommand
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
         var token=  await _authService.LoginAsync(request.UsernameOrEmail, request.Password,900);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token,
            };
        }
    }
}
