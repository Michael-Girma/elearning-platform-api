using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; }

        [MinLength(8)]
        [Required]
        public string NewPassword { get; set; }

        [MinLength(8)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}