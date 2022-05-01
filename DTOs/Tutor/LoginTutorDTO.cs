using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadLoginTutorDTO
    {
        public JWTToken Auth { get; set; }

        public Tutor Tutor { get; set; }
    }
}