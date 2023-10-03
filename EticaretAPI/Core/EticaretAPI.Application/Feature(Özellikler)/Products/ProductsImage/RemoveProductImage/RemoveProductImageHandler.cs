using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.RemoveProductImage
{
    public class RemoveProductImageHandler : IRequestHandler<RemoveProductImageRequest, RemoveProductImageResponse>
    {
       readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImageHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductImageResponse> Handle(RemoveProductImageRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.FilesImage)
        .FirstOrDefaultAsync(p => p.ID == Guid.Parse(request.Id));
            FileImage? fileImage = product?.FilesImage.FirstOrDefault(p => p.ID == Guid.Parse(request.imageId));
            if(fileImage != null)
            product?.FilesImage.Remove(fileImage);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
