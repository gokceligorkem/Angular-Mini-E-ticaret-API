using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.GetProductImage
{
    public class GetProductCommandHandler : IRequestHandler<GetProductCommandRequest, List<GetProductCommandResponse>>
    {
        IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IConfiguration configuration;

        public GetProductCommandHandler(IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public async Task<List<GetProductCommandResponse>> Handle(GetProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product =
         await _productReadRepository.Table.Include(p => p.FilesImage)
         .FirstOrDefaultAsync(p => p.ID == Guid.Parse(request.id));

            if (product != null)
            {
                List<GetProductCommandResponse> responses = product.FilesImage.Select(p => new GetProductCommandResponse
                {
                    Path = Path.Combine(_webHostEnvironment.WebRootPath, "photo-images", p.Path),
                    FileName = p.Name,
                    ID = p.ID
                }).ToList();
                return responses;
            }
            else
            {
             
                return new List<GetProductCommandResponse>();
            }
        }
    }
}
