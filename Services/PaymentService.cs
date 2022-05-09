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

        public PaymentLink GeneratePaymentLink(CreatePaymentLinkDTO paymentLinkDTO, CheckoutOptions checkoutOptions, CheckoutItem checkoutItem)
        {
            var paymentLink = _mapper.Map<PaymentLink>(paymentLinkDTO);
            paymentLink.MerchantCode = checkoutOptions.SellerCode;
            paymentLink.Link = CheckoutHelper.GetCheckoutUrl(checkoutOptions, checkoutItem);
            return paymentLink;
        }

        public bool VerifyPaymentDetail(PaymentDetail paymentDetail)
        {
            throw new NotImplementedException();
        }
    }
}