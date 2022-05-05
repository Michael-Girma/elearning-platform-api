namespace elearning_platform.DTO
{
    public class ReadOnlineSessionDTO
    {
        public Guid OnlineSessionId { get; set; }

        public Guid PaymentOrderId { get; set; }

        public Guid SessionId { get; set; }

        public ReadSessionDTO Session { get; set; }

        public string VideoChatLink { get; set; }
    }
}