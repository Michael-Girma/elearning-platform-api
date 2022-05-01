using System.ComponentModel.DataAnnotations;

namespace elearning_platform.Models
{
    public class EducationLevel
    {
        [Key]
        public Guid EducationLevelId { get; set; }
        public string Level { get; set; }
    }
}