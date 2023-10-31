using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Orders.Query.GetByIdOrder
{
    public class GetByIdQueryResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public object BasketItems { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OrderCode { get; set; }
    }
}
