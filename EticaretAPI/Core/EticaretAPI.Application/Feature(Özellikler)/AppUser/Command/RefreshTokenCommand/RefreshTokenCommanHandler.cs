using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.RefreshTokenCommand
{
    public class RefreshTokenCommanHandler : IRequestHandler<RefreshTokenCommanRequest, RefreshTokenCommanResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenCommanHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenCommanResponse> Handle(RefreshTokenCommanRequest request, CancellationToken cancellationToken)
        {
            Token token= await _authService.RefreshTokenLogin(request.RefreshToken);
            return new()
            {
                Token = token
            };
        }
    }
}
