using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadStudentDTO
    {
        public Guid StudentId { get; set; }
        public Guid EducationLevelId { get; set; }
        public Guid Uid { get; set; }
        public ReadUserDTO User { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public IEnumerable<ReadSessionFeedbackDTO> SessionFeedbacks { get; set; }
    }
}