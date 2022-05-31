using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadEnquiryInsightDTO
    {
        public Guid Id { get; set; }

        public bool TutorTeachesOnline { get; set; }

        public bool Verified { get; set; }

        public int TutorSessionCount { get; set; }

        public ReadTaughtSubjectDTO TaughtSubject { get; set; }
    }
}