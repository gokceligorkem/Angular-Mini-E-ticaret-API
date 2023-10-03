using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.RemoveProductImage
{
    public class RemoveProductImageRequest:IRequest<RemoveProductImageResponse>
    {
        [FromRoute]
        public string Id { get; set; }
        [FromQuery]
        public string imageId { get; set; }
    }
}
