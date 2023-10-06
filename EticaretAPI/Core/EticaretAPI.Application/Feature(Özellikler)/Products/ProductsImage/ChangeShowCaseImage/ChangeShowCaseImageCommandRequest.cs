using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.ChangeShowCaseImage
{
    public class ChangeShowCaseImageCommandRequest:IRequest<ChangeShowCaseImageCommandResponse>
    {
    
        public string imageId { get; set; }
        public string productId { get; set; }
    }
}
