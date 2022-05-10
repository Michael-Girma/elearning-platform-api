using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;
using YenePaySdk;

namespace elearning_platform.Profiles
{
    public class PaymentDetailProfile : Profile
    {
        public PaymentDetailProfile()
        {
            CreateMap<PaymentDetail, IPNModel>();
            CreateMap<ReadPaymentDetailDTO, PaymentDetail>();
        }
    }
}