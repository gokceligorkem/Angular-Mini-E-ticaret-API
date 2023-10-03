using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.GetProductImage
{
    public class GetProductCommandResponse
    {
        public Guid ID{ get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }

    }
}
