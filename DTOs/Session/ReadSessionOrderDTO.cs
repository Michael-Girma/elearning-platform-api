using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadSessionOrderDTO
    {
        public Guid SessionOrderId { get; set; }

        public string OrderStatus { get; set; }

        public Guid? PaymentDetailId { get; set; }

        public Guid OnlineSessionId { get; set; }
        public virtual ReadOnlineSessionDTO OnlineSession { get; set; }

        public virtual ICollection<ReadSessionPaymentLinkDTO> PaymentLinks { get; set; }

        public virtual PaymentDetail? PaymentDetail { get; set; }
    }
}