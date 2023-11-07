using EticaretAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO createOrder);
        Task<ListOrderDTO> GetAllOrdersAsync(int page,int size);
        Task<SingleOrderDTO> GetByIdOrder(string id);
        Task<(bool,CompletedOrderDTO)> CompleteOrderAsync(string id);
    }
}
