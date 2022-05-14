using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class InternalFileMetadata : BaseEntity
    {
        [Key]
        public Guid FileId { get; set; }

        [Required]
        public string ExternalId { get; set; }

        public Guid? UploadedByUid { get; set; }

        [ForeignKey("UploadedByUid")]
        public virtual User? UploadedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Rev { get; set; }

        [Required]
        public string Path { get; set; }

        public string ContentHash { get; set; }

        [Required]
        public string Filename { get; set; }

        [Required]
        public string OriginalFileName { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}