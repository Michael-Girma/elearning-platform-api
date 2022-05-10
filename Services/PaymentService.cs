using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Repo;
using YenePaySdk;

namespace elearning_platform.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentLinkRepo _paymentLinkRepo;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentLinkRepo paymentLinkRepo, IMapper mapper)
        {
            _paymentLinkRepo = paymentLinkRepo;
            _mapper = mapper;
        }

        public PaymentLink GeneratePaymentLink(PaymentLink paymentLink, CheckoutOptions checkoutOptions, CheckoutItem checkoutItem)
        {
            throw new NotImplementedException();

        }

        public SessionPaymentLink GenerateSessionPaymentLink(SessionPaymentLink paymentLink, CheckoutOptions checkoutOptions, CheckoutItem checkoutItem)
        {
            paymentLink.MerchantCode = checkoutOptions.SellerCode;
            paymentLink.Link = CheckoutHelper.GetCheckoutUrl(checkoutOptions, checkoutItem);
            _paymentLinkRepo.CreateSessionPaymentLink(paymentLink);
            return paymentLink;
        }

        public Task<bool> VerifyPaymentDetail(PaymentDetail paymentDetail)
        {
            var ipn = _mapper.Map<IPNModel>(paymentDetail);
            return CheckoutHelper.IsIPNAuthentic(ipn);
        }
    }
}