using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface IUserService 
    {
        Admin AddAdmin(CreateAdminDTO adminDTO);

        User GetUserDetails(Guid id);

        ReadPaymentAccountDetailDTO UpdatePaymentDetails(Guid userId, CreatePaymentAccountDetailDTO paymentDTO);

    }
}