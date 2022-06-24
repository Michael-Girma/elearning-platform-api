using elearning_platform.Data;
using elearning_platform.Models;
using System.Security.Cryptography;


namespace elearning_platform.Repo
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AppDbContext _ctx;

        public PaymentRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<PaymentDetail> GetAllPaymentDetails()
        {
            return _ctx.PaymentDetails.ToList();
        }

        public User? GetUserByMerchantCode(string merchantCode)
        {
            return _ctx.Users.FirstOrDefault(e => e.PaymentAccountDetail != null && e.PaymentAccountDetail.YenePaySellerCode == merchantCode);
        }

        public PaymentDetail SavePaymentDetail(PaymentDetail paymentDetail)
        {
            var entity = _ctx.PaymentDetails.SingleOrDefault(e => e.TransactionCode == paymentDetail.TransactionCode);
            if(entity != null){
                return paymentDetail;
            }
            _ctx.PaymentDetails.Add(paymentDetail);
            _ctx.SaveChanges();
            return paymentDetail;
        }
    }
}