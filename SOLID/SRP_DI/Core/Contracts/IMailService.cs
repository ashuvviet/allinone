using System.Threading.Tasks;

namespace Core
{
    public interface IMailService
    {
        bool IsValid(string value);
        Task<bool> SendMail(string to, string subject, string body);
    }
}