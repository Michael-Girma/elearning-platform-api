using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class CreateSubjectDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid EducationLevelId { get; set; }

        public string ThumbnailPath { get; set; }
    }
}