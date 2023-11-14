using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Query.AppUserGetAllQuery
{
    public class AppUserGetAllUserQueryRequest:IRequest<AppUserGetAllUserQueryResponse>
    {
        public int Page { get; set; } =0;
        public int Size { get; set; } = 5;
    }
}