using System.ComponentModel.DataAnnotations;

namespace elearning_platform.Models
{
    public class PaymentOrder
    {
        [Key]
        public Guid PaymentOrderId { get; set; }
    }
}