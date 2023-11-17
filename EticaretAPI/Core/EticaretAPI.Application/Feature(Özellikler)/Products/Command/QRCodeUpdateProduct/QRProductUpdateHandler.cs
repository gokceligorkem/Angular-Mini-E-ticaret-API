using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.Command.QRCodeUpdateProduct
{
    public class QRProductUpdateHandler : IRequestHandler<QRProductUpdateRequest, QRProductUpdateResponse>
    {
        readonly IProductService _productService;

        public QRProductUpdateHandler(IProductService productService)
        {
            _productService = productService;
        }

        public  async Task<QRProductUpdateResponse> Handle(QRProductUpdateRequest request, CancellationToken cancellationToken)
        {
           await _productService.StockUpdateToProductAsync(request.productId, request.Stock);
            return new();
        }
    }
}
