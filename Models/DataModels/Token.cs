namespace elearning_platform.Models
{
    public class JWTToken : BaseEntity
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public Guid Uid { get; set; }
    }
}