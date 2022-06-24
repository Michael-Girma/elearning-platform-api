using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.DTO
{
    public class CreateLessonDocumentDTO
    {
        public Guid InternalFileMetadataId { get; set; }
    }
}