using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadLessonDocumentDTO: BaseEntityDTO
    {
        public Guid DocumentId { get; set; }
        public Guid TaughtSubjectId { get; set; }
        public Guid InternalFileMetadataId { get; set; }

        public virtual ReadTaughtSubjectDTO TaughtSubject { get; set; }

        public virtual InternalFileMetadata InternalFileMetadata { get; set; }
    }
}