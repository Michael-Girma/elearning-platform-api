using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IPaymentRepo
    {
        PaymentDetail SavePaymentDetail(PaymentDetail paymentDetail);
    }
}