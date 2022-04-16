using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.DTO
{
    public class StudentSignupDTO
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int EducationLevelId { get; set; }
    }
}