using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.Queries.GetByIdProduct
{
    public class GetByIdProductHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductResponse>
    {
        IProductReadRepository _repository;

        public GetByIdProductHandler(IProductReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetByIdProductResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
          Product product=await _repository.GetByIdAsync(request.ProductId, false);
            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock=product.Stock
            };
        }
    }
}
