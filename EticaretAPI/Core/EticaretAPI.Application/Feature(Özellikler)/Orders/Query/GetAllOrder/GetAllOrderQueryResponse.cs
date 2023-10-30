using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Orders.Query.GetAllOrder
{
    public class GetAllOrderQueryResponse 
    {
      public int totalOrderCount { get; set; }
      public object Orders { get; set; }
    }
}
