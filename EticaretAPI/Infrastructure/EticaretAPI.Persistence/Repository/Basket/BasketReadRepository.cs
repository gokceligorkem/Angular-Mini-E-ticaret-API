using EticaretAPI.Application.Repository;
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
    public class BasketReadRepository : ReadRepository<Domain.Entities.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
