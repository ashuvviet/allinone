using Core.Attributes;
using Core.Contracts;
using System;
using System.Composition;

[assembly : AutoGenerate]
namespace MailService.AWS.SES
{
    [Service(Contract = typeof(IMailServce))]
    public class SESMailService : IMailServce
    {
        public string Name =>  "SESMailService";

        public string SendMail(string sender, string receiver, string subject, string body)
        {
            // AWS .net SDS


            return "From AWS SES Service";
        }
    }
}
