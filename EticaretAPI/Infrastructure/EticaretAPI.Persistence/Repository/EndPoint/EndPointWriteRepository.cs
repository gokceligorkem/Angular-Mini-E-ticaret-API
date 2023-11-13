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
    public class EndPointWriteRepository : WriteRepository<EndPoint>, IEndPointWriteRepository
    {
        public EndPointWriteRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
