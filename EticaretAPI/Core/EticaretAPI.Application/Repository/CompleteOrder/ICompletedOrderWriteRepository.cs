using EticaretAPI.Application.Repository.Basket;
using EticaretAPI.Application.Repository.BasketItem;
using EticaretAPI.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Repository
{
    public interface ICompletedOrderWriteRepository : IWriteRepository<CompletedOrder>
    {
    }
}
