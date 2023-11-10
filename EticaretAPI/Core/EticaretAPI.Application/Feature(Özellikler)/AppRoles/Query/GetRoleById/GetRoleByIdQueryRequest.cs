using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoleById
{
    public class GetRoleByIdQueryRequest:IRequest<GetRoleByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}