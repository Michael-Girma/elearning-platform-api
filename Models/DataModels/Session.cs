using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Session : BaseEntity
    {
        public Session(){}

        [Key]
        public Guid SessionId { get; set; }

        [Required]
        [ForeignKey("TutorRequest")]
        public Guid TutorRequestId { get; set; }

        public virtual TutorRequest TutorRequest { get; set; }

        [Required]
        public DateTime BookedTime { get; set; }

        [Required]
        public string BookingStatus { get; set; }

        public virtual OnlineSession? OnlineSession { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }

        public virtual ICollection<Assessment> Assessments { get; set; }

        public int SessionLength { get; set; }

        public string? StudentNotes { get; set; }

        public string? Recommendations { get; set; }

        public virtual SessionFeedback SessionFeedback { get; set; }
        public enum BookingStatuses
        {
            Booked,
            Cancelled,
            AwaitingInitialPayment
        }

        public float GetSessionCost()
        {
            return TutorRequest.SessionLength * TutorRequest.TaughtSubject.PricePerHour;
        }
    }
}