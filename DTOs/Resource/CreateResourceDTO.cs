using elearning_platform.Attributes.Validation;

namespace elearning_platform.DTO
{
    public class CreateResourceDTO
    {
        public string Name { get; set; }

        public Guid SessionId { get; set; }

        public Guid FileId { get; set; }

        [ResourceRecommendation]
        public string RecommendationLevel { get; set; }
    }
}