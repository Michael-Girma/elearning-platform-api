using System.ComponentModel.DataAnnotations;

namespace elearning_platform.Models
{
    public class PaymentLink : BaseEntity
    {
        [Key]
        public Guid PaymentLinkId { get; set; }

        [Required]
        public string OnSuccessReturn { get; set; }
        public string OnCancelReturn { get; set; }

        [Required]
        public int ExpiresAt { get; set; }

        [Required]
        public string Link { get; set; }

        public virtual Guid OrderId { get; set; }

        [Required]
        public string MerchantCode { get; set; }
    }
}