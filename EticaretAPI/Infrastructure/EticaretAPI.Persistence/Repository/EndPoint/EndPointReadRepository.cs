using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repository
{
    public class EndPointReadRepository : ReadRepository<EndPoint>, IEndPointReadRepository
    {
        public EndPointReadRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
