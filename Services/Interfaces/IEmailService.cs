namespace elearning_platform.Services
{
    public interface IEmailService
    {
        bool SendEmail(string email, string subject, string body, bool isBodyHtml = false);
    }
}