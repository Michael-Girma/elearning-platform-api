using elearning_platform.DTO;
using elearning_platform.Models;
using YenePaySdk;

namespace elearning_platform.Services
{
    public interface IPaymentService
    {
        PaymentLink GeneratePaymentLink(PaymentLink paymentLinkDTO, CheckoutOptions checkoutOptions, CheckoutItem checkoutItem);
        SessionPaymentLink GenerateSessionPaymentLink(SessionPaymentLink paymentLinkDTO, CheckoutOptions checkoutOptions, CheckoutItem checkoutItem);

        Task<bool> VerifyPaymentDetail(PaymentDetail paymentDetail);
    }
}