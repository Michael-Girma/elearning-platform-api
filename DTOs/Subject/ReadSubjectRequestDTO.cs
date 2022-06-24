using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadSubjectRequestDTO: BaseEntityDTO
    {
        public Guid SubjectRequestId { get; set; }

        public string SubjectName { get; set; }

        public Guid RequestAuthorId { get; set; }

        public bool Approved { get; set; }
        public bool Addressed { get; set; }

        public Guid EducationLevelId { get; set;}

        public string Description { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }
        public virtual ReadTutorDTO Tutor { get; set; }
    }
}