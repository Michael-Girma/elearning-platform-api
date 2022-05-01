using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Subject
    {
        [Key]
        public Guid SubjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid CreatedBy { get; set; }

        public User CreatedByUser { get; set; }

        [Required]
        [ForeignKey("EducationLevel")]
        public Guid EducationLevelId { get; set; }

        public EducationLevel EducationLevel { get; set; }
    }
}