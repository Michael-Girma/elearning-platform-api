namespace elearning_platform.Models
{
    public class JWTToken
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public int Uid { get; set; }
    }
}