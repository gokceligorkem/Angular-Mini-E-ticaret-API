using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Basket.Query.GetBasketItem
{
    public class GetBasketItemsCommandHandler : IRequestHandler<GetBasketItemsCommandRequest, List<GetBasketItemsCommandResponse>>
    {
        readonly IBasketService _basketService;

        public GetBasketItemsCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemsCommandResponse>> Handle(GetBasketItemsCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItem = await _basketService.GetBasketItemsAsync();

            return  basketItem.Select(ba => new GetBasketItemsCommandResponse
            {
                BasketItemId=ba.ID.ToString(),
                Name=ba.Product.Name,
                Price=ba.Product.Price,
                Quantity=ba.Quantity
            }).ToList();
        }
    }
}
