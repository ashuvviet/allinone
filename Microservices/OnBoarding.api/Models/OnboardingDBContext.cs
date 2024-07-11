using Microsoft.EntityFrameworkCore;

namespace OnBoarding.api.Models
{
    public class OnboardingDBContext : DbContext
    {
        public OnboardingDBContext(DbContextOptions<OnboardingDBContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
