using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IMailService : IService
    {
        Task<bool> SendMail(string to, string subject, string body);
    }
}
