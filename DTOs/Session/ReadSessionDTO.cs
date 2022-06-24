using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadSessionDTO
    {
        public Guid SessionId { get; set; }
        public Guid TutorRequestId { get; set; }

        public ReadTutorRequestDTO TutorRequest { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime BookedTime { get; set; }

        public string BookingStatus { get; set; }

        public ReadOnlineSessionDTO? OnlineSession { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public ICollection<Assessment> Assessments { get; set; }

        public string StudentNotes { get; set; }

        public int SessionLength { get; set; }

        public string Recommendations { get; set; }

        public virtual ReadSessionFeedbackDTO SessionFeedback { get; set;  }
    }
}