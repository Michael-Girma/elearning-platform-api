using elearning_platform.DTO;
using elearning_platform.Models;
using YenePaySdk;

namespace elearning_platform.Services
{
    public interface IPaymentService
    {
        PaymentLink GeneratePaymentLink(CreatePaymentLinkDTO paymentLinkDTO, CheckoutOptions checkoutOptions, CheckoutItem checkoutItem);

        bool VerifyPaymentDetail(PaymentDetail paymentDetail);
    }
}