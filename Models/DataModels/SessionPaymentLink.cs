using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class SessionPaymentLink : PaymentLink
    {
        [Required]
        [ForeignKey("SessionOrder")]
        public override Guid OrderId { get; set; }

        public virtual SessionOrder SessionOrder { get; set; }
    }
}