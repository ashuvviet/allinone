using Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MailService.Extensions
{
    public static class Extensions
    {
        public static void RegisterMailService(this IServiceCollection services)
        {
            services.AddScoped<IMailService, SMTPMailService>();
        }
    }
}
