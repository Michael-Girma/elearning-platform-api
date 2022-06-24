using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.DTO
{
    public class StudentSignupDTO : SignupDTO
    {

        public Guid? EducationLevelId { get; set; }
    }
}