using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class OnlineSession : BaseEntity
    {
        [Key]
        public Guid OnlineSessionId { get; set; }

        [Required]
        [ForeignKey("PaymentOrder")]
        public Guid PaymentOrderId { get; set; }

        [ForeignKey("SessionOrder")]
        public Guid SessionOrderId { get; set; }

        [Required]
        public SessionOrder SessionOrder { get; set; }

        [Required]
        [ForeignKey("Session")]
        public Guid SessionId { get; set; }

        public Session Session { get; set; }

        public string? VideoChatLink { get; set; }
    }
}