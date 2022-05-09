using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class CreatePaymentLinkDTO
    {
        [Required]
        public string OnSuccessReturn { get; set; }
        public string OnCancelReturn { get; set; }
    }
}