using EmployeeManagementApi.Controllers;
using EmployeeManagementApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Employee.Api.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async void GetEmployee_Tests()
        {
            // Arrange
            var mockRepo = Substitute.For<IEmployeeRepository>();
            var list = new List<EmployeeManagementApi.Models.Employee>() { new EmployeeManagementApi.Models.Employee() };
            mockRepo.GetAll().Returns(s =>
            {
                return list;
            });

            var controller = new EmployeeController(mockRepo);

            // Act
            var result = await controller.GetAll();
            var r = result.Result as OkObjectResult;

            // Assert
            Assert.Same(list, r.Value);
        }

        [Fact]
        public async void GetEmployeeById_Tests()
        {
            // Arrange
            var mockRepo = Substitute.For<IEmployeeRepository>();
            var list = new List<EmployeeManagementApi.Models.Employee>() { new EmployeeManagementApi.Models.Employee() };
            mockRepo.Get(Arg.Any<int>()).Returns(s =>
            {
                return list.First();
            });

            var controller = new EmployeeController(mockRepo);

            // Act
            var result = await controller .GetEmployeeById(1);
            var r = result as OkObjectResult;

            // Assert
            Assert.Same(list.First(), r.Value);
        }

        [Fact]
        public async void InsertEmployee_Tests()
        {
            // Arrange
            var mockRepo = Substitute.For<IEmployeeRepository>();
            var list = new List<EmployeeManagementApi.Models.Employee>();
            mockRepo.InsertEmployee(Arg.Any<EmployeeManagementApi.Models.Employee>()).Returns(s =>
            {
                list.Add(s[0] as EmployeeManagementApi.Models.Employee);
                return 1;
            });

            var controller = new EmployeeController(mockRepo);

            // Act
            var result = await controller.InsertEmployee(new EmployeeManagementApi.Dto.EmployeeDto { FirstName = "test" });

            // Assert
            Assert.Single(list);
            Assert.NotNull(result);
        }
    }
}
