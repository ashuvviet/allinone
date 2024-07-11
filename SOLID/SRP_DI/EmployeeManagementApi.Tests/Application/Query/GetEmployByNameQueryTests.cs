using EmployeeManagementApi.Application.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementApi.Tests.Application.Query
{
    public class GetEmployByNameQueryTests
    {
        [Fact]
        public void GetEmployByNameQuery_Tests()
        {
            // Arrange
            var query = new GetEmployeeByNameQuery();

            // Act
            query.Name = "x";

            // Assert
            Assert.Equal("x", query.Name);
        }
    }
}
