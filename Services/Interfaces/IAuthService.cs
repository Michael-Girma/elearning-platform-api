using elearning_platform.DTO;
using System.Threading.Tasks;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface IAuthService
    {
        Task<bool> SendMfaAsync(User user, Mfa mfa);

        Task<bool> RequestReset(string email);
        Task<User> ResetPassword(ResetPasswordDTO resetDTO);
        User ChangePassword(User user, ChangePasswordDTO changePasswordDTO);
    }
}