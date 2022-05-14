using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Chat : BaseEntity
    {
        [Key]
        public Guid ChatId { get; set; }

        public Guid InitiatorUid { get; set; }

        [ForeignKey("InitiatorUid")]
        [Required]
        public virtual User Initiator { get; set; }

        public virtual ICollection<UserChat> Participants { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}