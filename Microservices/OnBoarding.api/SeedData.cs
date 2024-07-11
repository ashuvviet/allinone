using OnBoarding.api.Models;

namespace OnBoarding.api
{
    public class SeedData
    {
        public static void Initialize(OnboardingDBContext context)
        {
            context.Database.EnsureCreated();
            // Look for any employees.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            context.Employees.AddRange(
                               new Models.Employee
                               {
                                   FirstName = "Frank",
                                   LastName = "Th",
                                   Email = "frank@gmail.com"
                               });

            context.Employees.AddRange(
                             new Models.Employee
                             {
                                 FirstName = "Benjamin",
                                 LastName = "Her",
                                 Email = "ben@gmail.com"
                             });

            context.SaveChanges();
        }
    }
}
