using elearning_platform.Data;
using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public class PaymentLinkRepo : IPaymentLinkRepo
    {
        private readonly AppDbContext _ctx;

        public PaymentLinkRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public SessionPaymentLink CreateSessionPaymentLink(SessionPaymentLink paymentLink)
        {
            _ctx.SessionPaymentLinks.Add(paymentLink);
            _ctx.SaveChanges();
            return paymentLink;
        }

        public SessionPaymentLink? GetSessionPaymentLinkById(Guid paymentLinkId)
        {
            return _ctx.SessionPaymentLinks.FirstOrDefault(e => e.PaymentLinkId == paymentLinkId);
        }

        public SessionPaymentLink? UpdateSessionPaymentLink(SessionPaymentLink paymentLink)
        {
            var entity = _ctx.SessionPaymentLinks.FirstOrDefault(e => e.PaymentLinkId == paymentLink.PaymentLinkId);
            if (entity == null)
            {
                return null;
            }
            _ctx.Entry(entity).CurrentValues.SetValues(paymentLink);
            _ctx.SaveChanges();
            return paymentLink;
        }
    }
}