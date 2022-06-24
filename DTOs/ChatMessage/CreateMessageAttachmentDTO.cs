using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class CreateMessageAttachmentDTO
    {
        public string ExternalId { get; set; }

        public Guid? UploadedByUid { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Rev { get; set; }

        public string Path { get; set; }

        public string ContentHash { get; set; }

        public string Filename { get; set; }

        public string OriginalFileName { get; set; }
    }
}