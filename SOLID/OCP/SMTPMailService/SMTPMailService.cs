using Core;
using Core.Attributes;
using Core.Contracts;
using System;
using System.Composition;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

[assembly: AutoGenerate]
namespace SMTPMailService
{
    [Service(Contract = typeof(IMailService), Order = 100)]
    public class SMTPMailService : IMailService
    {
        private ILoggingService logger => Container.Resolve<ILoggingService>();

        public string Name => throw new NotImplementedException();

        public SMTPMailService()
        {
        }

        public async Task<bool> SendMail(string to, string subject, string body)
        {
            try
            {
                logger.Log("***** Entry to SMTP Mail Service******");
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
                logger.Log("***** Error from SMTP Mail Service");
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
