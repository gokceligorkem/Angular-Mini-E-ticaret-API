using EticaretAPI.Application.Repository;
using EticaretAPI.Application.RequestParameters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.Queries
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductHandler> _looger;


        public GetAllProductHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductHandler> looger)
        {
            _productReadRepository = productReadRepository;
            _looger = looger;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _looger.LogInformation("Tüm ürünler listelendi");
            
            var totalCount = _productReadRepository.GetAll(false).Count();

            var products = _productReadRepository.GetAll(false)
            .Skip(request.Page * request.Size) 
            .Take(request.Size) 
            .Select(p => new
            {
                p.ID,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedTime,
                p.UpdatedTime,
            })
            .ToList();

            return new ()
            {
                products = products,
                totalCount = totalCount
            };
        }
    }
}
