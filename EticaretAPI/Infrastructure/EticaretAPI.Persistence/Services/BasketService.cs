using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Repository;
using EticaretAPI.Application.Repository.Basket;
using EticaretAPI.Application.Repository.BasketItem;
using EticaretAPI.Application.ViewModels.Baskets;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWrite;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;
        readonly IBasketReadRepository _basketReadRepository;
        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWrite, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWrite = basketWrite;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
        }
        private async Task<Basket?> ContextUser()
        {
            var username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            if(!string.IsNullOrEmpty(username) )
            {
               AppUser? user=await _userManager.Users.Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var _basket = from basket in user.Baskets
                              join
                            order in _orderReadRepository.Table on basket.ID equals order.ID into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                 Basket= basket,
                                 Order= order
                              };
                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                {
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }
                 

                await _basketWrite.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hata ile karşılaşıldı.");
        }
        public async Task AddToBasketAsync(VM_BasketItem_Create basketItem)
        {
            Basket? basket = await ContextUser();
            if(basket != null)
            {
              BasketItem _basketItem= await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.ID && bi.ProductId == Guid.Parse(basketItem.ProductId)); 
                if(_basketItem != null)
                {
                    _basketItem.Quantity++;
                }
                else
                {
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId=basket.ID,
                        ProductId=Guid.Parse(basketItem.ProductId),
                        Quantity=basketItem.Quantity
                    });
                }
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket? basket = await ContextUser();
           Basket? result= 
                await _basketReadRepository.Table
                .Include(b => b.BasketItem)
                .ThenInclude(b => b.Product).
                FirstOrDefaultAsync(b => b.ID == basket.ID);

            return result.BasketItem.ToList();
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem? basketItem =await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if(basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
            else
            {
                throw new Exception("Silinemedi");
            }
        }

        public async Task UpdateQuantityAsync(VM_BasketItem_Update basketItem)
        {
            BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if(_basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
               await _basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
