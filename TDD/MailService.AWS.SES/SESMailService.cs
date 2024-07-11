using Core.Attributes;
using Core.Contracts;
using System;
using System.Composition;
using System.Threading.Tasks;

[assembly : AutoGenerate]
namespace MailService.AWS.SES
{
    [Service(Contract = typeof(IMailService))]
    public class SESMailService : IMailService
    {
        public string Name =>  "SESMailService";

        public Task<bool> SendMail(string sender, string receiver, string subject, string body)
        {
            // AWS .net SDS


            return Task.FromResult(true);
        }
    }
}
