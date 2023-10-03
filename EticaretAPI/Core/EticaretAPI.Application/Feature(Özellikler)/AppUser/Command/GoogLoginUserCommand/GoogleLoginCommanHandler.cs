using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.DTOs;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.GoogLoginUserCommand
{
    public class GoogleLoginCommanHandler : IRequestHandler<GoogleLoginCommanRequest, GoogleLoginCommanResponse>
    {
        readonly IAuthService _authService;

        public GoogleLoginCommanHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommanResponse> Handle(GoogleLoginCommanRequest request, CancellationToken cancellationToken)
        {
           var token=await _authService.GoogleLoginAsync(request.IdToken, 900);
            return new()
            {
                Token = token
            };
        }
    }
}
