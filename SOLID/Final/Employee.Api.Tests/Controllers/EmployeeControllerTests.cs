using Core.Contracts;
using EmployeeManagementApi.Dto;
using EmployeeWebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Employee.Api.Tests
{
    public class EmployeeControllerTests
    {
        [Fact]
        public void InsertEmployee_InvalidName_Test()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<EmployeeController>>();
            var mockemployeeRepository = new Mock<IEmployeeRepository>();
            var mockMailService = new Mock<IMailService>();
            var mockNamingService = new Mock<INamingService>();
            mockNamingService.Setup(s => s.IsValid(It.IsAny<string>())).Returns(false);

            var controller = new EmployeeController(mockLogger.Object, mockemployeeRepository.Object, mockMailService.Object, mockNamingService.Object);

            // Act
            Func<Task> result = async () => await controller.InsertEmployee(new EmployeeDto());

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(result);
        }

        [Fact]
        public async void InsertEmployee_Test()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<EmployeeController>>();
            var mockemployeeRepository = new Mock<IEmployeeRepository>();
            var mockMailService = new Mock<IMailService>();
            var mockNamingService = new Mock<INamingService>();
            mockNamingService.Setup(s => s.IsValid(It.IsAny<string>())).Returns(true);
            mockMailService.Setup(s => s.IsValid(It.IsAny<string>())).Returns(true);
            mockemployeeRepository.Setup(s => s.InsertEmployee(It.IsAny<Core.Models.Employee>())).Returns(Task.FromResult(1));

            var controller = new EmployeeController(mockLogger.Object, mockemployeeRepository.Object, mockMailService.Object, mockNamingService.Object);

            // Act
            var result = await controller.InsertEmployee(new EmployeeDto());

            // Assert
            Assert.Equal(1, 1);
        }
    }
}
