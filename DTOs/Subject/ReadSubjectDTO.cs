using System.ComponentModel.DataAnnotations;
using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadSubjectDTO : BaseEntityDTO
    {
        [Key]
        public Guid SubjectId { get; set; }

        [Required]
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        // [Required]
        // public ReadUserDTO CreatedByUser { get; set; }

        public virtual IEnumerable<ReadTaughtSubjectDTO> TaughtSubjects { get; set; }
        public virtual IEnumerable<ReadStarredSubject> StarredSubjects { get; set; }

        [Required]
        public Guid EducationLevelId { get; set; }

        public EducationLevel EducationLevel { get; set; }
    }
}