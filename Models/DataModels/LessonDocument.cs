using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class LessonDocument : BaseEntity
    {
        [Key]
        public Guid DocumentId { get; set; }

        [ForeignKey("TaughtSubject")]
        public Guid TaughtSubjectId { get; set; }

        [ForeignKey("InternalFileMetadata")]
        public Guid InternalFileMetadataId { get; set; }

        public virtual TaughtSubject TaughtSubject { get; set; }

        public virtual InternalFileMetadata InternalFileMetadata { get; set; }
    }
}