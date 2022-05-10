using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class SessionOrder : BaseEntity
    {
        [Key]
        public Guid SessionOrderId { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [ForeignKey("PaymentDetail")]
        public Guid? PaymentDetailId { get; set; }

        [ForeignKey("OnlineSession")]
        public Guid OnlineSessionId { get; set; }
        public virtual OnlineSession OnlineSession { get; set; }

        public virtual ICollection<SessionPaymentLink> PaymentLinks { get; set; }

        public virtual PaymentDetail? PaymentDetail { get; set; }

        public enum OrderStatuses
        {
            Paid,
            AwaitingPayment,
        }
    }
}