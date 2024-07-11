using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IMailService
    {
        Task<bool> SendMail(string to, string subject, string body);

        bool IsValid(string name);
    }
}
