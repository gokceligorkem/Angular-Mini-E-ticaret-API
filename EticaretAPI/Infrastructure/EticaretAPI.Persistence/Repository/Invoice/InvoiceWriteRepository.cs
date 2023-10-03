using EticaretAPI.Application.Repository.Invoice;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repository.Invoice
{
    public class InvoiceWriteRepository : WriteRepository<InvoiceFile>, IInvoiceWriteRepository
    {
        public InvoiceWriteRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
