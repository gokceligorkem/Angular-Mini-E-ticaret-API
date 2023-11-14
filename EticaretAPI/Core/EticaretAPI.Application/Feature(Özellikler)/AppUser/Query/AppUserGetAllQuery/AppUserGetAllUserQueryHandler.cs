using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Query.AppUserGetAllQuery
{
    public class AppUserGetAllUserQueryHandler : IRequestHandler<AppUserGetAllUserQueryRequest, AppUserGetAllUserQueryResponse>
    {
        readonly IUserService _userService;

        public AppUserGetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AppUserGetAllUserQueryResponse> Handle(AppUserGetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
          var getListUsers= await _userService.GetAllUserAsync(request.Page, request.Size);
            return new()
            {
                Users = getListUsers,
                TotalUsersCount = _userService.TotalUsersCount
            };
        }
    }
}
