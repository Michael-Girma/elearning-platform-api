using System.Threading.Tasks;


namespace elearning_platform.Services
{
    public interface IAuthService
    {
        Task<bool> SendMfaEmailAsync(string email, string body);
    }
}