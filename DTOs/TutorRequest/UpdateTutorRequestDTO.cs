using System.ComponentModel.DataAnnotations;
using elearning_platform.Attributes.Validation;

namespace elearning_platform.DTO
{
    public class UpdateTutorRequestDTO
    {
        [Required]
        public bool OnlineSession { get; set; }

        [Required]
        public int SessionLength { get; set; }

        [Required]
        public DateTime[] PreferredDates { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual string Status { get; set; }
    }
}