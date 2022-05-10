using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class PaymentAccountDetail : BaseEntity
    {
        public Guid Id { get; set; }

        public string? YenePaySellerCode { get; set; }

        [ForeignKey("User")]
        public Guid Uid { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}