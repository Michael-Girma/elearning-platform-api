using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class ReadPaymentLinkDTO
    {
        public Guid PaymentLinkId { get; set; }

        public string OnSuccessReturn { get; set; }

        public string OnCancelReturn { get; set; }

        public int ExpiresAt { get; set; }

        public string Link { get; set; }
    }
}