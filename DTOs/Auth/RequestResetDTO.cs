using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class RequestResetDTO 
    {
        [Required]
        public string Email { get; set; }
    }
}