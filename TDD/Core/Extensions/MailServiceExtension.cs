using Core;
using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class MailServiceExtension
    {
        public static void SendMailWithLogging(this IMailService mailService)
        {
            var result = mailService.SendMail("X", "Y", "", "");

            var loggingService = Container.Resolve<ILoggingService>();
            loggingService.Log("mail sent " + result);
        }
    }
}
