using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        readonly IRoleService _roleservice;

        public GetRoleByIdQueryHandler(IRoleService roleservice)
        {
            _roleservice = roleservice;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
          var data= await _roleservice.GetRoleById(request.Id);
            return new()
            {
                Id = data.id,
                Name = data.name
            };
        }
    }
}
