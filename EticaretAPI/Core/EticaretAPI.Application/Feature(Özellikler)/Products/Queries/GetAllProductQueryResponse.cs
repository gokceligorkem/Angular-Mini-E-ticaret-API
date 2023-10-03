using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.Queries
{
    public class GetAllProductQueryResponse
    {
        public int totalCount { get; set; }
        public object products { get; set; }
    }
}
