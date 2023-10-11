using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Basket.Query.GetBasketItem
{
    public class GetBasketItemsCommandResponse
    {
        public string BasketItemId { get; set; }
        public string  Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
