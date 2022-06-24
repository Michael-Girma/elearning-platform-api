using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public Admin AddAdmin(CreateAdminDTO adminDTO)
        {
            throw new NotImplementedException();
        }

        public User GetUserDetails(Guid id)
        {
            var user = _userRepo.GetUserById(id);
            return user;
        }

        public ReadPaymentAccountDetailDTO UpdatePaymentDetails(Guid userId, CreatePaymentAccountDetailDTO paymentDTO)
        {
            var user = _userRepo.GetUserById(userId);
            if(user.PaymentAccountDetail == null)
            {
                user.PaymentAccountDetail = new PaymentAccountDetail(){
                    YenePaySellerCode = paymentDTO.YenePaySellerCode
                };
            }
            else
            {
                user.PaymentAccountDetail.YenePaySellerCode = paymentDTO.YenePaySellerCode;
            }
            _userRepo.SaveUser(user);
            return _mapper.Map<ReadPaymentAccountDetailDTO>(user.PaymentAccountDetail);
        }
    }
}