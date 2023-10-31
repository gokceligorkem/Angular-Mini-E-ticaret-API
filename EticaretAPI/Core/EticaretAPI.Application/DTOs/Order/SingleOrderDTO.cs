using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.DTOs.Order
{
    public class SingleOrderDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public object BasketItems { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OrderCode { get; set; }
    }
}
