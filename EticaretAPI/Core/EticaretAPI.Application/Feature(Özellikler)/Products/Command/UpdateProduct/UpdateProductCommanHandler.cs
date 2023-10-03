using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.Command.UpdateProduct
{
    public class UpdateProductCommanHandler : IRequestHandler<UpdateProductCommanRequest, UpdateProductCommanResponse>
    {
        readonly IProductReadRepository _readrepository;
        readonly IProductWriteRepository _writerepository;
        readonly ILogger<UpdateProductCommanHandler> _logger;
        public UpdateProductCommanHandler(IProductReadRepository readrepository, IProductWriteRepository writerepository, ILogger<UpdateProductCommanHandler> logger)
        {
            _readrepository = readrepository;
            _writerepository = writerepository;
            _logger = logger;
        }

        public async Task<UpdateProductCommanResponse> Handle(UpdateProductCommanRequest request, CancellationToken cancellationToken)
        {
            Product product = await _readrepository.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;
            await _writerepository.SaveAsync();
            _logger.LogInformation("Product güncellendi..");
            return new();
        }
    }
}
