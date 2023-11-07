using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.Order;
using EticaretAPI.Application.Repository;
using EticaretAPI.Application.Repository.CompleteOrder;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace EticaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWritRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderReadRepository _completedOrderRead;
        readonly ICompletedOrderWriteRepository _completedOrderWrite;

        public OrderService(IOrderWritRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderReadRepository completedOrderRead, ICompletedOrderWriteRepository completedOrderWrite)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderRead = completedOrderRead;
            _completedOrderWrite = completedOrderWrite;
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
            var data2 = from order in data
                        join completedOrder in _completedOrderRead.Table
                        on order.ID equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            ID = order.ID,
                            CreatedTime = order.CreatedTime,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false
                        };
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new 
                {
                    ID = o.ID,
                    CreatedTime = o.CreatedTime,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItem.Sum(bi => bi.Product.Price * bi.Quantity),
                    Username = o.Basket.User.UserName,
                    o.Completed

                }).ToListAsync()
            };

        }

        public async Task<SingleOrderDTO> GetByIdOrder(string id)
        {
            var data =  _orderReadRepository.Table.
                Include(o => o.Basket).
                ThenInclude(o => o.BasketItem).
                ThenInclude(o => o.Product);
            var data2 =await (from order in data
                        join completeOrder in _completedOrderRead.Table on order.ID equals completeOrder.OrderId
                        into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            ID = order.ID,
                            CreatedTime = order.CreatedTime,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false,
                            Address = order.Address,
                            Description=order.Description

                        }).FirstOrDefaultAsync(o => o.ID == Guid.Parse(id));


            //
            return new()
            {
               Id=data2.ID.ToString(),
                BasketItems = data2.Basket.BasketItem.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
               Adress=data2.Address,
               CreatedTime=data2.CreatedTime,
               OrderCode=data2.OrderCode,
               Description=data2.Description,
               Completed=data2.Completed
            };

        }
        public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id)
        {
            Order? order = await _orderReadRepository.Table.Include(o => o.Basket).ThenInclude(b => b.User).FirstOrDefaultAsync(f=>f.ID==Guid.Parse(id));
            if(order != null)
            {
              await  _completedOrderWrite.AddAsync(new()
                {
                    OrderId=Guid.Parse(id)
                });
               return (await _completedOrderWrite.SaveAsync() > 0, 
                        new() { 
                            OrderCode=order.OrderCode,
                            OrderDate=order.CreatedTime,
                            Username=order.Basket.User.UserName,
                            UserSurname=order.Basket.User.NameSurname,
                            Email=order.Basket.User.Email

                        });
            }
            return (false,null);
        }
    }
}
