using System.Threading.Tasks;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface IAuthService
    {
        Task<bool> SendMfaAsync(User user, Mfa mfa);


    }
}