using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Business_Logic.DTO;
using Business_Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using File = DataAccess.Entities.File;

namespace Business_Logic.Services
{
    public class AzurePictureService : IFileService, IDisposable
    {
        private const string containerName = "product-images";

        private IConfiguration Configuration { get; }
        private IDataService<File, FileDTO> FileServiceImpl { get; }

        public AzurePictureService(IConfiguration Configuration, IGenericUnitOfWork GenericUnitOfWork, IMapper Mapper)
        {
            this.Configuration = Configuration;
            this.FileServiceImpl = new DataService<File, FileDTO>(GenericUnitOfWork, Mapper);
        }

        public async Task<FileDTO> UploadFile(IFormFile file, int ownerId)
        {
            string name = Path.GetRandomFileName(), extension = Path.GetExtension(file.FileName);
            string blobName = name + extension;

            var blob = await GetBlob(blobName);
            await blob.UploadAsync(file.OpenReadStream());
            FileDTO dto = new(blob.Uri.ToString(), blobName, ownerId);
            await FileServiceImpl.Add(dto);

            return dto;
        }
        public async Task<FileDTO[]> UploadFiles(IEnumerable<IFormFile> files, int ownerId)
        {
            List<FileDTO> dtos = new();

            foreach (var file in files)
            {
                var dto = await UploadFile(file, ownerId);
                dtos.Add(dto);
            }

            return dtos.ToArray();
        }

        public async Task DeleteFile(FileDTO file)
        {
            var blob = await GetBlob(file.Name);
            await blob.DeleteAsync();
        }
        public async Task DeleteFiles(IEnumerable<FileDTO> files)
        {
            foreach (var file in files)
                await DeleteFile(file);
        }

        public async Task<FileDTO> EntityByUrl(string url)
        {
            var all = await FileServiceImpl.GetAllAsync();
            var pic = all.FirstOrDefault(pic => pic.Url == url);
            return pic ?? throw new ArgumentException("Server doesn't contain such picture. Check the URL given.", nameof(url));
        }

        private async Task<BlobClient> GetBlob(string filename)
        {
            var client = await GetClient();
            var blob = client.GetBlobClient(filename);
            return blob;
        }
        private async Task<BlobContainerClient> GetClient()
        {
            BlobContainerClient client = new(Configuration.GetConnectionString("AzureFs"), containerName);

            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(PublicAccessType.Blob);

            return client;
        }

        public bool Exists(string fileUrl)
        {
            try
            {
                var pic = EntityByUrl(fileUrl).Result;
                var blob = GetBlob(pic.Name).Result;
                return blob.Exists().Value;
            }
            catch { return false; }
        }
        public bool Exists(FileDTO file)
        {
            return FileServiceImpl.Contains(file);
        }

        public void Dispose()
        {
            FileServiceImpl.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
