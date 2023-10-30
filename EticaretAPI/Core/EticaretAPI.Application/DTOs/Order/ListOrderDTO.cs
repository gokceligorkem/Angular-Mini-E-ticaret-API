using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.DTOs.Order
{
    public class ListOrderDTO
    {
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}
