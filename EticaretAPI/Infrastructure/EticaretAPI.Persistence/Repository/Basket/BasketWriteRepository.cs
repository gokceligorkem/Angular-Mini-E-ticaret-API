using EticaretAPI.Application.Repository.Basket;
using EticaretAPI.Application.Repository.BasketItem;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repository
{
    public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
