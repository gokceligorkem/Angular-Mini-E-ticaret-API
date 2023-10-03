using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.DTOs;
using EticaretAPI.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.FacebookLoginUserComamnd
{
    public class FacebookLoginCommanHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookloginCommandResponse>
    {
        readonly IAuthService _authService;

        public FacebookLoginCommanHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookloginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.FacebookLoginAsync(request.AuthToken,60);
            return new()
            {
                Token = token
            };
        }
    }
}
