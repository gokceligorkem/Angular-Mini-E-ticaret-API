using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.AuthorizationEndPointsCommand
{
    public class AssignRoleEndPointCommandHandler : IRequestHandler<AssignRoleEndPointCommandRequest, AssignRoleEndPointCommandResponse>
    {
        readonly IAuthorizationEndPointService _authorizaservice;

        public AssignRoleEndPointCommandHandler(IAuthorizationEndPointService authorizaservice)
        {
            _authorizaservice = authorizaservice;
        }

        public async Task<AssignRoleEndPointCommandResponse> Handle(AssignRoleEndPointCommandRequest request, CancellationToken cancellationToken)
        {
           await _authorizaservice.AssignRoleEndPointAsync(request.Roles, request.Code, request.Menu, request.Type);
            return new()
            {

            };
        }
    }
}
