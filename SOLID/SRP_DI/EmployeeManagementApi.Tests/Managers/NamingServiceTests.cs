using Core;
using Xunit;

namespace EmployeeManagementApi.Tests.Managers
{
    public class NamingServiceTests
    {
        [Theory]
        [InlineData("x", true)]
        public void IsValidTests(string input, bool result)
        {
            // Arrange
            var namingSerive = new NamingService();

            // Act
            var actualResult = namingSerive.IsValid(input);

            // Assert
            Assert.Equal(result, actualResult);
        }
    }
}
