using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Storage
{
    public interface IStorage
    {
        Task <List<(string fileName,string pathOrContainerName, long? size)>> UploadAsync( string pathOrContainerName,IFormFileCollection files);
        Task DeleteAsync(string pathOrContainerName,string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFiles(string pathOrContainerName,string fileName);
    }
}
