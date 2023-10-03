using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQueryRequest:IRequest<GetByIdProductResponse>
    {
        public string ProductId { get; set;}
    }
}
