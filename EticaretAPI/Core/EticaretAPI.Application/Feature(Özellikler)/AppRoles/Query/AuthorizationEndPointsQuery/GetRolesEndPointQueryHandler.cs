using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.AuthorizationEndPointsQuery
{
    public class GetRolesEndPointQueryHandler : IRequestHandler<GetRolesEndPointQueryRequest, GetRolesEndPointQueryResponse>
    {
        readonly IAuthorizationEndPointService _service;

        public GetRolesEndPointQueryHandler(IAuthorizationEndPointService service)
        {
            _service = service;
        }

        public async Task<GetRolesEndPointQueryResponse> Handle(GetRolesEndPointQueryRequest request, CancellationToken cancellationToken)
        {
            var datas= await _service.GetRolesEndPointAsync(request.Code,request.Menu);
            return new()
            {
                Roles=datas
            };
        }
    }
}
