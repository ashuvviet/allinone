using EmployeeManagementApi.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementApi.Tests.Application
{
    public class InsertEmployeeValidatorTests
    {
        [Theory]
        [InlineData("x", "", "a@g.com", 1)]
        [InlineData("x", "", "", 2)]
        [InlineData("", "", "", 3)]
        public void InsertEmployee_Test(string f, string l, string e, int errorCount)
        {
            // Arrange
            var validator = new InsertEmployeeCommandValidator();

            // Act
            var result = validator.Validate(new EmployeeManagementApi.Application.Commands.InsertEmployeCommand() { FirstName = f, LastName = l, Email = e });

            // Assert
            Assert.True(errorCount == result.Errors.Count);
        }
    }
}
