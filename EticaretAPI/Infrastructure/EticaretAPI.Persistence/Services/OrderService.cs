using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.Order;
using EticaretAPI.Application.Repository;
using EticaretAPI.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWritRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWritRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrder)
        {

            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                ID = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
               
            });
            await _orderWriteRepository.SaveAsync();

        }
    }
}
