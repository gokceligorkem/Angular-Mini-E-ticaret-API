using EticaretAPI.Application.Repository;
using EticaretAPI.Application.Repository.CompleteOrder;
using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.Persistence.Repository
{
    public class CompletedOrderWriteRepository : WriteRepository<Domain.Entities.CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
