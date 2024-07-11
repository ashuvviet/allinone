using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataBaseCore.DBContext;
using Core.Contracts;

namespace DataBaseCore.Extensions
{
    public static class Extensions
    {
        public static void RegisterDatabase(this IServiceCollection services)
        {
            services.AddDbContext<EmployeeDBContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
