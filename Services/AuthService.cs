using System.Net.Mail;
using elearning_platform.Configs;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public class AuthService : IAuthService
    {
        private readonly SMTPConfig _smtpConfig;
        private readonly IEmailService _emailService;

        public AuthService(SMTPConfig smtpConfig, IEmailService emailService)
        {
            _smtpConfig = smtpConfig;
            _emailService = emailService;
        }

        public async Task<bool> SendMfaAsync(User user, Mfa mfa)
        {
            var body = $"Here's your new code: {mfa.PinCode}";
            var subject = "Multi-Factor Auth";
            var success = await _emailService.SendEmail(user.Email, body, subject);
            return success;
        }
    }
}
