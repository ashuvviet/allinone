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
    public class CompanyTests
    {
        [Fact]
        public void Company_Test()
        {
            // Arrange
            var c = new Company("Test");

            // Act
            // Assert
            Assert.Single(c.Employees);
        }

        [Fact]
        public void AddEmployee_Test()
        {
            // Arrange
            var c = new Company("Test");
            var e = new Employee("First");

            // Act
            c.Add(e);

            // Assert
            Assert.NotEmpty(c.Name);
            Assert.NotNull(c["First"]);
            Assert.NotEmpty(c.Employees);
            Assert.Contains(e, c.Employees);
        }

        [Fact]
        public void AddEmployee_Exception_Test()
        {
            // Arrange
            var c = new Company("Test");
            var e = new Employee("");

            // Act
            Action add = () => c.Add(e);

            // Assert
            Assert.Throws<InvalidOperationException>(add);
        }

        [Fact]
        public void RemoveEmployee_Test()
        {
            // Arrange
            var c = new Company("Test");
            var e = new Employee("First");
            c.Add(e);

            // Act
            c.Remove("First");
            //c.Remove("First Emp");

            // Assert
            Assert.Null(c["First"]);
            Assert.Single(c.Employees);
            Assert.DoesNotContain(e, c.Employees);
        }

        [Fact]
        public void RemoveEmployee_Exception_Test()
        {
            // Arrange
            var c = new Company("Test");
            var e = new Employee("First");
            c.Add(e);

            // Act
            Action remove = () => c.Remove("Second");

            // Assert
            Assert.Throws<InvalidOperationException>(remove);
        }
    }
}
