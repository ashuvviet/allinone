using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IMailService : IService
    {
        Task<bool> SendMail(string sender, string target, string subject, string body);
    }
}
