using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.DeleteRoleCommand
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        readonly IRoleService _roleservice;

        public DeleteRoleCommandHandler(IRoleService roleservice)
        {
            _roleservice = roleservice;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
           var result=await _roleservice.DeleteRole(request.Id);
            return new()
            {
                Succeeded = result
            };
        }
    }
}
