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
    public class FileImageWriteRepository : WriteRepository<FileImage>, IFileImageWriteRepository
    {
        public FileImageWriteRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
