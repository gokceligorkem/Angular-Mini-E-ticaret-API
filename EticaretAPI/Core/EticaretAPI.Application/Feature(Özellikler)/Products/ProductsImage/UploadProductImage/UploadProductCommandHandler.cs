using EticaretAPI.Application.Abstraction.Storage;
using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.ProductsImage.UploadProductImage
{
    public class UploadProductCommandHandler : IRequestHandler<UploadProductCommandRequest, UploadProductCommandResponse>
    {
        readonly IStorageService _storageService;
        IProductReadRepository _productReadRepository;
        IFileImageWriteRepository _fileImageWriteRepository;
        public UploadProductCommandHandler(IProductReadRepository productReadRepository, IFileImageWriteRepository fileImageWriteRepository, IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _fileImageWriteRepository = fileImageWriteRepository;
            _storageService = storageService;
        }

        public async Task<UploadProductCommandResponse> Handle(UploadProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await _productReadRepository.GetByIdAsync(request.Id);

            List<(string fileName, string pathOrContainerName, long? size)> result = await _storageService.UploadAsync("photo-images", request.Files);

            await _fileImageWriteRepository.AddRangeAsync(result.Select(f => new FileImage
            {
                Name = f.fileName,
                Path = f.pathOrContainerName,
                Stogare = _storageService.StogareName,
                Products = new List<Product>() { product }
            }).ToList());

            await _fileImageWriteRepository.SaveAsync();

            return new();
        }
    }
}
