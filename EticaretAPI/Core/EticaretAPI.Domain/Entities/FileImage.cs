using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class FileImage:FileBase
    {
        public bool Showcase { get; set; }
        public ICollection<Product> Products { get; set; }
        virtual public string Caption { get; set; }
        virtual public string Alt { get; set; }
    }
}
