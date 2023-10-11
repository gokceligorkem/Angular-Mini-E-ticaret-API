using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Basket.Query.GetBasketItem
{
    public class GetBasketItemsCommandRequest:IRequest<List<GetBasketItemsCommandResponse>>
    {
    }
}
