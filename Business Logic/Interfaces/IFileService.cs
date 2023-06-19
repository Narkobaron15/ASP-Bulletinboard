using Business_Logic.DTO;
using Microsoft.AspNetCore.Http;

namespace Business_Logic.Interfaces;

public interface IFileService : IDisposable
{
    // Upload file(s) with owner's entity id
    Task<FileDTO> UploadFile(IFormFile file, int ownerId);
    Task<FileDTO[]> UploadFiles(IEnumerable<IFormFile> files, int ownerId);

    // Delete file(s)
    Task DeleteFile(FileDTO file);
    Task DeleteFiles(IEnumerable<FileDTO> files);

    // Checking if file exists
    bool Exists(FileDTO file);
    bool Exists(string fileUrl);

    // Getting file from the db by url
    Task<FileDTO> EntityByUrl(string url);
}
