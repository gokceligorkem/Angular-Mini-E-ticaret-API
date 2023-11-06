using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Feature_Özellikler_.Orders.Command.CompletedOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Orders.Command.CompletedOrders
{
    public class CompletedOrderCommandHandler : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>

    {
        readonly IOrderService _orderService;

        public CompletedOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
           await _orderService.CompleteOrderAsync(request.Id);
            return new ();
        }
    }
}
