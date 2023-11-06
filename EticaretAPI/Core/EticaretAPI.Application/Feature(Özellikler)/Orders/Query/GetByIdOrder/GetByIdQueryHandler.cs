using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Orders.Query.GetByIdOrder
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
         var data=await   _orderService.GetByIdOrder(request.Id);
            return new()
            {
                Id=data.Id,
                Adress=data.Adress,
                BasketItems=data.BasketItems,
                CreatedTime=data.CreatedTime,
                Description=data.Description,
                OrderCode= data.OrderCode,
                Completed = data.Completed
            };
        }
    }
}
