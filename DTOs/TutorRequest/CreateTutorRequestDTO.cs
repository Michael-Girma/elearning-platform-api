using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class CreateTutorRequestDTO
    {
        [Required]
        public bool OnlineSession { get; set; }

        [Required]
        public Guid TaughtSubjectId { get; set; }

        [Required]
        public int SessionLength { get; set; }

        [Required]
        public DateTime[] PreferredDates { get; set; }

        [Required]
        public string Note { get; set; }
    }
}