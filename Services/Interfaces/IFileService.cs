using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface IFileService
    {
        Task<TemporaryFileRef> GetDownloadLink(InternalFileMetadata fileMetadata);
        Task<InternalFileMetadata> UploadFile(IFormFile content);
        Task<string> UploadStaticFile(IFormFile content);
        Task<TemporaryFileRef?> GetTemporaryRef(Guid id);
        string GetFileContent(IFormFile formFile);
    }
}