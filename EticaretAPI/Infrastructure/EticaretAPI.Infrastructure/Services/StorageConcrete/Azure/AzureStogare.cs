using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EticaretAPI.Application.Abstraction.Storage;
using EticaretAPI.Application.Abstraction.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.StorageConcrete.Azure
{
    public class AzureStogare : Stogare.Storage,IAzureStogare
    {
        readonly BlobServiceClient _blobServiceClient;//İlgili azure account bağlanmayı sağlar.
        BlobContainerClient _blobContainerClient;//Accounttaki hedef container üzerinden işlem yapmayı sağlayan servis.

        public AzureStogare(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Stogare:Azure"]);
           
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient=_blobServiceClient.GetBlobContainerClient(containerName);
           BlobClient blobClient= _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync(); 
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(b=>b.Name).ToList();
        }

        public bool HasFiles(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(b => b.Name==fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName, long? size)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient =  _blobServiceClient.CreateBlobContainer(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            List<(string fileName, string pathOrContainerName, long? size)> datas = new();
            foreach(IFormFile file in files)
            {
              string newName=  GenerateFullFilePath(containerName, file.Name);
               BlobClient blobClient= _blobContainerClient.GetBlobClient(newName);
               await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((file.Name, containerName,file.Length));
            
            }
            return datas;
        }

       
    }
}
