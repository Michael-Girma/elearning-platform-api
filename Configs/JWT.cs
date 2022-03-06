namespace elearning_platform.Configs
{
    public class JWTConfig
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiryTime { get; set; }
    }
}