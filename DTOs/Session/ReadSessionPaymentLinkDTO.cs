using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadSessionPaymentLinkDTO : ReadPaymentDetailDTO
    {

        public virtual ReadSessionOrderDTO SessionOrder { get; set; }
    }
}