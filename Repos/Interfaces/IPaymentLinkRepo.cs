using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IPaymentLinkRepo
    {
        SessionPaymentLink? GetSessionPaymentLinkById(Guid paymentLinkId);

        SessionPaymentLink CreateSessionPaymentLink(SessionPaymentLink paymentLink);

        SessionPaymentLink? UpdateSessionPaymentLink(SessionPaymentLink paymentLink);
    }
}