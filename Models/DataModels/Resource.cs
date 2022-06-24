using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Resource : BaseEntity
    {
        [Key]
        public Guid ResourceId { get; set; }

        public string Name { get; set; }

        [ForeignKey("Session")]
        public Guid SessionId { get; set; }

        [ForeignKey("InternalFileMetadata")]
        public Guid FileId { get; set; }

        public string RecommendationLevel { get; set; }


        public enum RecommendationLevelValues 
        {
            Mandatory,
            HighlyRecommended,
            NiceToLookAt
        }
    }
}