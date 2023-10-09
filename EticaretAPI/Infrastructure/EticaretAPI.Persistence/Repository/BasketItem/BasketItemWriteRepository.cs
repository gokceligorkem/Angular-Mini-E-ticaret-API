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
    public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
