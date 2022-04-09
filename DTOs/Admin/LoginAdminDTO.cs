namespace elearning_platform.Models
{
    public class LoginAdminDTO
    {
        public Admin Admin { get; set; }

        public JWTToken Auth { get; set; }
    }
}