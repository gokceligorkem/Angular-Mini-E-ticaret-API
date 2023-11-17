using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using System.Text.Json;

namespace EticaretAPI.Persistence.Services
{
    public class ProductService:IProductService
    {
        readonly IProductReadRepository _readRepository;
        readonly IQRCodeService _qrService;
        readonly IProductWriteRepository _writeRepository;
        public ProductService(IProductReadRepository readRepository, IQRCodeService qrService, IProductWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _qrService = qrService;
            _writeRepository = writeRepository;
        }

        public async Task<byte[]> QRCodeToProductAsync(string productId)
        {
           Product product  =await _readRepository.GetByIdAsync(productId);
            if (product == null)
                 throw new Exception("Product not found");

            var plainObject = new
            {
                product.ID,
                product.Name,
                product.Stock,
                product.Price,
                product.CreatedTime
            };
            string plainText = JsonSerializer.Serialize(plainObject);
           return _qrService.GeneratQRCode(plainText);
        }

        public async Task StockUpdateToProductAsync(string productId, int stock)
        {
            Product product = await _readRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Product not found");
            product.Stock = stock;
            await _writeRepository.SaveAsync();
        }
    }
}
