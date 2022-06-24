using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class ReadResourceDTO
    {
        [Key]
        public Guid ResourceId { get; set; }

        public string Name { get; set; }

        public Guid SessionId { get; set; }

        public Guid FileId { get; set; }

        public string RecommendationLevel { get; set; }

        public virtual ReadStudentDTO Student { get; set; }
    }
}