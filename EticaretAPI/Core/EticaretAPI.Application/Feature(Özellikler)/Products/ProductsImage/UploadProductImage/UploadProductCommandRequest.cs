using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.UploadProductImage
{
    public class UploadProductCommandRequest:IRequest<UploadProductCommandResponse>
    {
        
        public string Id { get; set; }
        
        public IFormFileCollection? Files { get; set; }
    }
}
