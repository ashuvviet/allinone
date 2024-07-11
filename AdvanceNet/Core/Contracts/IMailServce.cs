namespace Core.Contracts
{
    public interface IMailServce : IService
    {
        string SendMail(string sender, string target, string subject, string body);
    }
}
