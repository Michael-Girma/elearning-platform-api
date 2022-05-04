using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Student : BaseEntity
    {
        [Key]
        public Guid StudentId { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ForeignKey("EducationLevel")]
        public Guid EducationLevelId { get; set; }

        [ForeignKey("User")]
        public Guid Uid { get; set; }


        public User User { get; set; }
        public EducationLevel EducationLevel { get; set; }
    }
}