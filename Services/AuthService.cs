using System.Net.Mail;
using elearning_platform.Configs;

namespace elearning_platform.Services
{
    public class AuthService : IAuthService
    {
        private readonly SMTPConfig _smtpConfig;

        public AuthService(SMTPConfig smtpConfig)
        {
            _smtpConfig = smtpConfig;
        }

        public async Task<bool> SendMfaEmailAsync(string email, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_smtpConfig.EmailFromAddress);
            message.Body = body;
            message.To.Add(email);
            message.IsBodyHtml = false;
            message.Subject = "Multi-factor Auth";
            var client = _smtpConfig.getClientForConfig();
            try
            {
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}