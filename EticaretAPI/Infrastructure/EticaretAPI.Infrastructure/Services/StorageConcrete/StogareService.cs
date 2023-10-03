using EticaretAPI.Application.Abstraction.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.StorageConcrete
{
    public class StogareService : IStorageService
    {   readonly IStorage _storage;

        public StogareService(IStorage storage)
        {
            _storage = storage;
        }

        public string StogareName { get=>_storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
           await _storage.DeleteAsync(pathOrContainerName, fileName);
        }

        public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName);
        

        public bool HasFiles(string pathOrContainerName, string fileName)
            => _storage.HasFiles(pathOrContainerName, fileName);

      

        Task<List<(string fileName, string pathOrContainerName, long? size)>> IStorage.UploadAsync(string pathOrContainerName, IFormFileCollection files)
        =>
           _storage.UploadAsync(pathOrContainerName, files);
        
    }
}
