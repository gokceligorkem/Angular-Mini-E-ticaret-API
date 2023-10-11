using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Basket.Command.AddItemToBasket
{
    public class AddBasketItemToBasketCommandRequest:IRequest<AddBasketItemToBasketCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
