using System.Net;
using System.Net.Mail;

namespace elearning_platform.Configs
{
    public class SMTPConfig
    {
        public string SmtpAddress { get; set; }

        public int PortNumber { get; set; }

        public bool EnableSSL { get; set; }

        public string EmailFromAddress { get; set; }

        public string EmailPassEnv { get; set; }

        public string? Password
        {
            get
            {
                return Environment.GetEnvironmentVariable(EmailPassEnv);
            }
        }

        private NetworkCredential? _credentials;
        public NetworkCredential Credentials
        {
            get
            {

                if (_credentials == null)
                {
                    _credentials = new NetworkCredential(EmailFromAddress, Password);
                }
                return _credentials;
            }
            private set
            {
                _credentials = value;
            }
        }

        public SmtpClient getClientForConfig()
        {
            var client = new SmtpClient(SmtpAddress, PortNumber);
            client.Credentials = _credentials;
            client.EnableSsl = EnableSSL;
            return client;
        }

    }
}