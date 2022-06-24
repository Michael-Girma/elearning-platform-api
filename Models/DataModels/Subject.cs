using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Subject : BaseEntity
    {
        [Key]
        public Guid SubjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        public string ThumbnailPath { get; set; }

        [ForeignKey("CreatedBy")]

        public virtual User CreatedByUser { get; set; }

        [Required]
        [ForeignKey("EducationLevel")]
        public Guid EducationLevelId { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }

        public virtual IEnumerable<TaughtSubject> TaughtSubjects { get; set; }

        public virtual IEnumerable<StarredSubject> StarredSubjects { get; set; }
    }
}