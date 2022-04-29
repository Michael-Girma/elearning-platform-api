using System.Text;
using Dropbox.Api;
using Dropbox.Api.Files;
using elearning_platform.Models;
using elearning_platform.Repo;
using elearning_platform.Utils;

namespace elearning_platform.Services
{
    public class FileService : IFileService
    {
        private readonly DropboxClient _dbx;
        private readonly IFileRepo _fileRepo;

        public FileService(IFileRepo fileRepo)
        {
            _dbx = new DropboxClient(Environment.GetEnvironmentVariable("DROPBOX_ACCESS_TOKEN"));
            _fileRepo = fileRepo;
        }

        public Task<TemporaryFileRef> GetDownloadLink(InternalFileMetadata fileMetadata)
        {
            var resourceLinkTask = _dbx.Files.GetTemporaryLinkAsync(fileMetadata.ExternalId).Result;
            var tempRef = new TemporaryFileRef()
            {
                FileId = fileMetadata.FileId,
                TemporaryDownloadLink = resourceLinkTask.Link,
                ContentHash = fileMetadata.ContentHash
            };
            return Task.FromResult(tempRef);
        }

        public Task<InternalFileMetadata> UploadFile(IFormFile formFile)
        {
            using (var mem = new MemoryStream())
            {
                formFile.CopyTo(mem);
                var fileBytes = mem.ToArray();
                var dropBoxFileName = $"{GetFileNameWithoutExt(formFile.FileName)}-{DataUtils.ToMD5Hash(fileBytes)}.{GetFileExtension(formFile.FileName)}";
                var updated = _dbx.Files.UploadAsync(
                    "/Capstone/" + $"{dropBoxFileName}",
                    WriteMode.Overwrite.Instance,
                    body: mem
                ).Result;
                var fileMetadata = new InternalFileMetadata()
                {
                    ContentHash = updated.ContentHash,
                    ExternalId = updated.Id,
                    Filename = dropBoxFileName,
                    OriginalFileName = formFile.FileName,
                    Path = updated.PathDisplay,
                    Rev = updated.Rev,
                };
                _fileRepo.WriteInternalFileMetadata(fileMetadata);
                return Task.FromResult<InternalFileMetadata>(fileMetadata);
            }
        }

        public async Task<TemporaryFileRef?> GetTemporaryRef(Guid id)
        {
            var fileMetadata = _fileRepo.GetInternalFileMetadata(id);
            if (fileMetadata != null)
            {
                var temporaryRef = await GetDownloadLink(fileMetadata);
                return temporaryRef;
            }
            return null;
        }

        public string GetFileContent(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            return result.ToString();
        }

        public string GetFileExtension(string fileName)
        {
            var sections = fileName.Split('.');
            return sections.Last();
        }

        public string GetFileNameWithoutExt(string fileName)
        {
            var sections = fileName.Split('.').SkipLast(1);
            return string.Join('.', sections);
        }
    }


}