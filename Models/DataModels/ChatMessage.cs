using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class ChatMessage : BaseEntity
    {
        [Key]
        public Guid MessageId { get; set; }
        public string Message { get; set; }

        public virtual IEnumerable<InternalFileMetadata> Attachments { get; set; }

        [ForeignKey("User")]
        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }

        public virtual Chat Chat { get; set; }
    }
}