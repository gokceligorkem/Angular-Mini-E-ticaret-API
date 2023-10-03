using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.GetProductImage
{
    public class GetProductCommandRequest:IRequest<List<GetProductCommandResponse>>
    {
        [FromRoute]
        public string id { get; set; }
    }
}
