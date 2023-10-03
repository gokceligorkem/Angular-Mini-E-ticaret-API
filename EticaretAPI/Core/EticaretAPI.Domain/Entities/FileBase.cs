using EticaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class FileBase:BaseEntity
    {
       public string Name { get; set; }
       public string Path { get; set; }
       public double Size { get; set; }
        public string Stogare { get;set; }
    }
}
