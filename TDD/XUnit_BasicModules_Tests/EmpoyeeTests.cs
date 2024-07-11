using BasicModule.BasicClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnit_BasicModules_Tests
{
    [ExcludeFromCodeCoverage]
    public class EmpoyeeTests
    {
        [Fact]
        public void Employee_Test()
        {
            // Arrange
            var e = new Employee("Test");

            // Act
            // Assert
            Assert.Equal("Test", e.Name);
            Assert.IsType<Guid>(e.Id);

            Assert.NotNull(e.Id.ToString());
        }
    }
}
