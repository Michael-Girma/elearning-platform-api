using System.ComponentModel.DataAnnotations;

namespace elearning_platform.Models
{
    public class PaymentDetail : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string MerchantOrderId { get; set; }

        [Required]
        public string MerchantId { get; set; }

        [Required]
        public string MerchantCode { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public string TransactionCode { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string Signature { get; set; }
    }
}