using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Entities.ViewModels
{
    public class MailManager
    {
        private readonly IConfiguration configuration;

        public MailManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool Send(string to, string subject, string content)
        {
            try
            {
                var host = configuration["Gmail:Host"];
                var port = int.Parse(configuration["Gmail:Port"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enable,
                    Credentials = new NetworkCredential(username, password)
                };

                var mailMessage = new MailMessage("harsh.study008@gmail.com", to);
                mailMessage.Subject = subject; // set the subject passed as a parameter
                mailMessage.Body = content;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}