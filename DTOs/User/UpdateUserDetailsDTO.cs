using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class UpdateUserDetailsDTO
    {
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ProfileImage { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
    }
}