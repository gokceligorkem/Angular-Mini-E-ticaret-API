using EticaretAPI.Application.Abstraction.Storage.Local;
using EticaretAPI.Infrastructure.Operation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;



namespace EticaretAPI.Infrastructure.Services.StorageConcrete.Local
{
    public class LocalStogare : ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStogare(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
        {
            File.Delete($"{path}\\{fileName}");
           
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.GetFiles().Select(f=>f.Name).ToList();
        }

        public bool HasFiles(string path, string fileName)
        =>File.Exists($"{path}\\{fileName}");
        
        private string GenerateFullFilePath(string path, string fileName)
        {
            Guid guid = Guid.NewGuid();
            string extension = Path.GetExtension(fileName).ToLower();
            string replaceExtension = NameOperation.CharacterRegulatory(fileName);

            return Path.Combine(path, $"{replaceExtension + "-"}{guid}{extension}");
        }
       
        private async Task<long> SaveFileAndGetFileSizeAsync(string fullPath, IFormFile file)
        {
            try
            {
                int bufferSize = 2 * 1024 * 1024; // 2MB kadar destekler.
                long fileSize = 0;

                await using (FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, useAsync: false))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    fileSize = fileStream.Length; 
                }

                return fileSize;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
        public async Task<List<(string fileName, string pathOrContainerName, long? size)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);


            List<(string fileName, string path, long? size)> datas = new();
            
            foreach (IFormFile file in files)
            {

                string fullPath = GenerateFullFilePath(uploadPath, file.FileName);


                long fileSize = await SaveFileAndGetFileSizeAsync(fullPath, file);

                datas.Add((file.FileName, $"{path}/{file.FileName}", fileSize));

            }

            return datas;
        }
    }
    
}
