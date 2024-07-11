using Microsoft.EntityFrameworkCore;
using OnBoarding.api.Models;

namespace Onboarding_api.Tests
{
    public class OnboardingDBContextTests
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OnboardingDBContext>()
                .UseInMemoryDatabase(databaseName: "Test1")
                .Options;

            using (var context = new OnboardingDBContext(options))
            {
                context.Employees.Add(new Employee { FirstName = "Frank", LastName = "Th" });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new OnboardingDBContext(options))
            {
                context.Employees.Add(new Employee { FirstName = "Test", LastName = "Th" });
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new OnboardingDBContext(options))
            {
                Assert.Equal(2, context.Employees.Count());
            }
        }
    }
}