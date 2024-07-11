using Core;
using Core.Models;
using EmployeeManagementApi.Application.Query;
using EmployeeManagementApi.Application.QueryHandlers;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementApi.Tests.Application.QueryHandlers
{
    public class GetEmployeeByNameQueryHandlerTests
    {
        [Fact]
        public async void GetEmployeeByNameQueryHandler_Test()
        {
            // Arrange
            var mockRepo = Substitute.For<IEmployeeRepository>();
            var query = new GetEmployeeByNameQuery();
            var handler = new GetEmployeeByNameQueryHandler(mockRepo);
            mockRepo.GetByName(Arg.Any<string>()).Returns(x => new FullTimeEmployee() { Id = 100 });

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Id);
        }
    }
}
