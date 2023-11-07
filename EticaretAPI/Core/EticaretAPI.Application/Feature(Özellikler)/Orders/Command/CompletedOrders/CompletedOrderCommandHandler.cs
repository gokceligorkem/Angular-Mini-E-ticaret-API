using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.Order;
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
        readonly IMailService _mailService;

        public CompletedOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
           (bool succeseed, CompletedOrderDTO dto) =await _orderService.CompleteOrderAsync(request.Id);
            if(succeseed)
            {
              await  _mailService.SendCompleteOrderMailAsync(dto.Email,dto.OrderCode,dto.OrderDate,dto.Username,dto.UserSurname);
            }
            return new ();
        }
    }
}
