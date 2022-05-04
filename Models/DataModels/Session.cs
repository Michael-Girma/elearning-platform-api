using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Session : BaseEntity
    {
        [Key]
        public Guid SessionId { get; set; }

        [Required]
        [ForeignKey("TutorRequest")]
        public Guid TutorRequestId { get; set; }

        public TutorRequest TutorRequest { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        [Required]
        public string BookingStatus { get; set; }
    }
}