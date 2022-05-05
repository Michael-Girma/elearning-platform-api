using elearning_platform.Attributes.Validation;

namespace elearning_platform.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }

        public int? pinCode { get; set; }

        public string Password { get; set; }
    }
}