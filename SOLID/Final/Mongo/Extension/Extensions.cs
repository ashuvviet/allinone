using Microsoft.Extensions.DependencyInjection;
using DataBaseCore.DBContext;
using Core.Contracts;

namespace DataBaseCore.Extensions
{
    public static class Extensions
    {
        public static void RegisterDatabase(this IServiceCollection services)
        {
            services.AddSingleton<EmployeeDBContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
