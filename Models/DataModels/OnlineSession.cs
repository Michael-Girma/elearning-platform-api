using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class OnlineSession : BaseEntity
    {
        [Key]
        public Guid OnlineSessionId { get; set; }

        [Required]
        public virtual SessionOrder SessionOrder { get; set; }

        [Required]
        [ForeignKey("Session")]
        public Guid SessionId { get; set; }

        public virtual Session Session { get; set; }

        public string? VideoChatLink { get; set; }
    }
}