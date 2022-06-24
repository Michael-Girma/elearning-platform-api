using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using static elearning_platform.Models.Resource;

namespace elearning_platform.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class ResourceRecommendation : ValidationAttribute
    {
        public RecommendationLevelValues[] AllowedValues { get; set; } = (RecommendationLevelValues[])Enum.GetValues(typeof(RecommendationLevelValues));

        public ResourceRecommendation(params RecommendationLevelValues[] possibleValues)
        {
            AllowedValues = possibleValues;
        }

        public ResourceRecommendation() { }
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            var status = (String)value;
            foreach (var allowedValue in AllowedValues)
            {
                if (status == allowedValue.ToString())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
