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