using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadTutorRequestDTO
    {
        public Guid TutorRequestId { get; set; }

        public Guid StudentId { get; set; }
        public ReadStudentDTO Student { get; set; }
        public Guid TaughtSubjectId { get; set; }
        public ReadTaughtSubjectDTO TaughtSubject { get; set; }

        public string Status { get; set; }
        //AWAITING_STUDENT || AWAITING_TUTOR || ACCEPTED
        public bool OnlineSession { get; set; }

        public int SessionLength { get; set; }

        public DateTime[] PreferredDates { get; set; }

        public string Note { get; set; }

        public ICollection<ReadSessionDTO> Sessions { get; set; }
    }
}