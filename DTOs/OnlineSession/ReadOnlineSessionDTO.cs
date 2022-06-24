using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadOnlineSessionDTO
    {
        public Guid OnlineSessionId { get; set; }

        public Guid PaymentOrderId { get; set; }

        public Guid SessionId { get; set; }

        public ReadSessionDTO Session { get; set; }

        public ReadSessionOrderDTO SessionOrder { get; set; }


        public string VideoChatLink { get; set; }
    }
}