using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoles
{
    public class GetRolesQueryRequest:IRequest<GetRolesQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}