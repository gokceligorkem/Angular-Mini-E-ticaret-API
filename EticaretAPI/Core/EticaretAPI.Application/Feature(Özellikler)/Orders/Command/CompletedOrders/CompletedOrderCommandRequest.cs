using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.Orders.Command.CompletedOrder
{
    public class CompletedOrderCommandRequest:IRequest<CompletedOrderCommandResponse>
    {
        public string Id { get; set; }
    }
}