using System.Net;
using System.Net.Mail;

namespace Entities.ViewModels
{
    public class EmailManager
    {
        public static void SendEmail(List<string> toList, string Subject, string Body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            foreach (var mailTo in toList)
            {
                mail.To.Add(mailTo);
            }
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("jenil.vora0408@gmail.com");
            mail.Subject = Subject;
            mail.Body = Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jenil.vora0408@gmail.com", "maynrbuigynhdzgf");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}