using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class UserDetailsDTO
    {

        public Student? Student { get; set; }

        public Admin? Admin { get; set; }

        public Tutor? Tutor { get; set; }
        public User User { get; set; }

        public JWTToken? Auth { get; set; }
    }
}