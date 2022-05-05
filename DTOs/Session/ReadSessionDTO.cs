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
    }
}