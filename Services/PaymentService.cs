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
        private readonly IPaymentRepo _paymentRepo;

        public PaymentService(IPaymentLinkRepo paymentLinkRepo, IPaymentRepo paymentRepo, IMapper mapper)
        {
            _paymentLinkRepo = paymentLinkRepo;
            _mapper = mapper;
            _paymentRepo = paymentRepo;
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

        public IEnumerable<ReadPaymentDetailDTO> GetAllPayments()
        {
            var payments = _paymentRepo.GetAllPaymentDetails().ToList();
            var readPaymentDTOs = _mapper.Map<IEnumerable<ReadPaymentDetailDTO>>(payments);
            foreach(var paymentDTO in readPaymentDTOs)
            {
                paymentDTO.Merchant = _mapper.Map<ReadUserDTO>(_paymentRepo.GetUserByMerchantCode(paymentDTO.MerchantCode));
            }
            return readPaymentDTOs;
        }
    }
}