﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_MAIN_ENTITIES.Models.ViewModels
{
    public class MailHelper
    {
        private IConfiguration configuration;

        public MailHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public bool Send(string to, string content)
        {
            try
            {
                var host = configuration["Gmail:Host"];
                var port = int.Parse(configuration["Gmail:Port"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                var smtpClient = new System.Net.Mail.SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enable,
                    Credentials = new NetworkCredential(username, password)
                };

                var mailMessage = new MailMessage("t12281554@gmail.com", to);
                mailMessage.Subject = "CI Platform";
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