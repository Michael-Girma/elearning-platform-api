using System.Net.Mail;
using elearning_platform.Configs;

namespace elearning_platform.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPConfig _smtpConfig;

        public EmailService(SMTPConfig smtpConfig)
        {
            _smtpConfig = smtpConfig;
        }

        public bool SendEmail(string email, string body, string subject)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_smtpConfig.EmailFromAddress);
            message.Body = body;
            message.To.Add(email);
            message.IsBodyHtml = false;
            message.Subject = subject;
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