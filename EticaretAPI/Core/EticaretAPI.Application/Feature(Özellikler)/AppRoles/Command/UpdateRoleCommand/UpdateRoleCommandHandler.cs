using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.UpdateRoleCommand
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        readonly IRoleService _roleservice;

        public UpdateRoleCommandHandler(IRoleService roleservice)
        {
            _roleservice = roleservice;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
          var result= await _roleservice.UpdateRole(request.Id, request.roleName);
            return new()
            {
                Succeeded=result
            };
        }
    }
}
