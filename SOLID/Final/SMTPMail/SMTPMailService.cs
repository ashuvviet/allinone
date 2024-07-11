using Core.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailService
{
    public class SMTPMailService : IMailService
    {
        private readonly ILogger<SMTPMailService> logger;

        public SMTPMailService(ILogger<SMTPMailService> logger)
        {
            this.logger = logger;
        }

        public async Task<bool> SendMail(string to, string subject, string body)
        {
            try
            {
                logger.LogInformation("***** Entry to SMTP Mail Service******");
                SmtpClient client = new SmtpClient();
                client.Host = "<host>";
                client.Port = int.MaxValue;

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("<username>", "<password>");                

                MailMessage message = CreateMailMessage(to, subject, body);
                client.Send(message);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                logger.LogError("***** Error from SMTP Mail Service");
                return false;
            }
        }

        private MailMessage CreateMailMessage(string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("<registered mail id>");
            mailMessage.To.Add(to);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            return mailMessage;
        }

        public bool IsValid(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.Contains("@"))
            {
                return false;
            }

            return true;
        }
    }
}
