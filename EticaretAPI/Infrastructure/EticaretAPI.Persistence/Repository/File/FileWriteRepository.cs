using EticaretAPI.Application.Repository.File;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repository.File
{
    public class FileWriteRepository : WriteRepository<FileBase>, IFileBaseWriteRepository
    {
        public FileWriteRepository(EticaretDbContext context) : base(context)
        {
        }
    }
}
