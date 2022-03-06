using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class LoginStudentDTO
    {
        public JWTToken Auth { get; set; }

        public Student Student { get; set; }
    }
}