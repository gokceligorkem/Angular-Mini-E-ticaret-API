using EticaretAPI.Application.ViewModels.Baskets;
using EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddToBasketAsync(VM_BasketItem_Create basketItem);
        public Task UpdateQuantityAsync(VM_BasketItem_Update basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
        public Basket? GetUserActiveBasket { get; }

    }
}
