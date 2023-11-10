using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoleById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.CreateRoleCommand
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
    {
        readonly IRoleService _roleservice;

        public CreateRoleCommandHandler(IRoleService roleservice)
        {
            _roleservice = roleservice;
        }

        public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
          var result= await _roleservice.CreateRole(request.Name);
            return new()
            {
                Succeeded = result
            };
        }
    }
}
