using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class ResetPasswordDTO
    {
        [MinLength(8)]
        public string password { get; set; }

        [Required]
        public string Token { get; set; }
    }
}