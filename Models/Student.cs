using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ForeignKey("EducationLevel")]
        public int EducationLevelId { get; set; }

        [ForeignKey("User")]
        public int Uid { get; set; }


        public User User { get; set; }
        public EducationLevel EducationLevel { get; set; }
    }
}