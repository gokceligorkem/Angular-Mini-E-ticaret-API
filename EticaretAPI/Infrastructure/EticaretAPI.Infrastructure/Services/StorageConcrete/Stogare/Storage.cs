using EticaretAPI.Infrastructure.Operation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.StorageConcrete.Stogare
{
    public class Storage
    {
        protected string GenerateFullFilePath(string path, string fileName)
        {
            Guid guid = Guid.NewGuid();
            string extension = Path.GetExtension(fileName).ToLower();
            string replaceExtension = NameOperation.CharacterRegulatory(fileName);

            return Path.Combine(path, $"{replaceExtension + "-"}{guid}{extension}");
        }

       
    }
}
