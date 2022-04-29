using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IFileRepo
    {
        public InternalFileMetadata? GetInternalFileMetadata(Guid id);
        public InternalFileMetadata WriteInternalFileMetadata(InternalFileMetadata file);
    }
}