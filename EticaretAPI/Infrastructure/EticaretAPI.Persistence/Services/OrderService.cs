﻿using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.Order;
using EticaretAPI.Application.Repository;
using Microsoft.EntityFrameworkCore;

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
            var orderCode = (new Random().Next(0,10000) * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                ID = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode

            }) ; 
            await _orderWriteRepository.SaveAsync();

        }

        public async Task<ListOrderDTO> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                  .ThenInclude(o => o.User)
                  .Include(o => o.Basket)
                  .ThenInclude(b => b.BasketItem)
                  .ThenInclude(p => p.Product);
              
             var data= query.Skip(page * size).Take(size);
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new 
                {
                    CreatedTime = o.CreatedTime,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItem.Sum(bi => bi.Product.Price * bi.Quantity),
                    Username = o.Basket.User.UserName

                }).ToListAsync()
            };

        }
    }
}
