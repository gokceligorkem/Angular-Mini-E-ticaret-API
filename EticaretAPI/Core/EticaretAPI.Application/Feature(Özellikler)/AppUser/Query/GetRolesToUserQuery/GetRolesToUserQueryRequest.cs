using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Query.GetRolesToUserQuery
{
    public class GetRolesToUserQueryRequest:IRequest<GetRolesToUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}