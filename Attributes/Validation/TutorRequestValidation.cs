using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using static elearning_platform.Models.TutorRequest;

namespace elearning_platform.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class TutorRequestStatus : ValidationAttribute
    {
        public RequestStatusValues[] AllowedValues { get; set; } = (RequestStatusValues[])Enum.GetValues(typeof(RequestStatusValues));

        public TutorRequestStatus(params RequestStatusValues[] possibleValues)
        {
            AllowedValues = possibleValues;
        }

        public TutorRequestStatus() { }
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
