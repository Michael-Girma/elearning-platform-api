using elearning_platform.Data;
using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public class FileRepo : IFileRepo
    {
        private readonly AppDbContext _ctx;

        public FileRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public InternalFileMetadata? GetInternalFileMetadata(Guid id)
        {
            var fileMetadata = _ctx.InternalFiles.FirstOrDefault(file => file.FileId == id);
            return fileMetadata;
        }

        public InternalFileMetadata WriteInternalFileMetadata(InternalFileMetadata fileMetadata)
        {
            _ctx.InternalFiles.Add(fileMetadata);
            _ctx.SaveChanges();
            return fileMetadata;
        }
    }
}