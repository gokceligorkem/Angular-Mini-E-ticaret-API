using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.AuthorizationEndPointsQuery
{
    public class GetRolesEndPointQueryRequest:IRequest<GetRolesEndPointQueryResponse>
    {
        public string Code { get; set; }
        public string Menu { get; set; }
    }
}