using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Basket.Command.AddItemToBasket
{
    public class AddBasketItemToBasketCommandHandler : IRequestHandler<AddBasketItemToBasketCommandRequest, AddBasketItemToBasketCommandResponse>
    {
        readonly IBasketService _basketService;

        public AddBasketItemToBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<AddBasketItemToBasketCommandResponse> Handle(AddBasketItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
           await _basketService.AddToBasketAsync(new()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });
            return new();
        }
    }
}
