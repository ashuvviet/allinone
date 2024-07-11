using BasicModule;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace XUnit_BasicModules_Tests
{
    [ExcludeFromCodeCoverage]
    public class MathInstanceTests
    {
        [Fact(DisplayName = "Add_Any_Number_Test")]
        public void Add_Test()
        {
            // Arrange
            var mathinstanceObj = new MathInstance();

            // Act
            var result = mathinstanceObj.Add(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

        [Theory(DisplayName = "Add_Positive_Numbers")]
        [InlineData(1, 2 , 3)]
        [InlineData(11, 12, 23)]
        [InlineData(0, 1000, 1000)]
        [InlineData(1000, 0, 1000)]
        [InlineData(99, 1, 100)]

        public void AddPositiveNumbers_Test_PostiveCase(int a, int b, int expectedResult)
        {
            // Arrange
            var mathinstanceObj = new MathInstance();

            // Act
            var result = mathinstanceObj.AddPositiveNumbers(a, b);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact(DisplayName = "Add_Negative_Numbers")]
        public void AddPositiveNumbers_Test_FailureCase()
        {
            // Arrange
            var mathinstanceObj = new MathInstance();

            // Act
            Action result = () => mathinstanceObj.AddPositiveNumbers(-1, 2);

            // Assert
            Assert.Throws<InvalidOperationException>(result);
        }

        [Fact(DisplayName = "Add_Positive_Numbers_Out_Result")]
        public void AddPositiveNumbers_Out_result_Test_PostiveCase()
        {
            // Arrange
            var mathinstanceObj = new MathInstance();

            // Act
            mathinstanceObj.AddPositiveNumbers(1, 2, out int result);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact(DisplayName = "Add_Negative_Numbers_Out_Result")]
        public void AddPositiveNumbers_Out_result_Test_NegativeCase()
        {
            // Arrange
            var mathinstanceObj = new MathInstance();

            // Act
            Action result = () => mathinstanceObj.AddPositiveNumbers(-1, -2, out int result);

            // Assert
            Assert.Throws<InvalidOperationException>(result);
        }

        [Fact]
        public void Max_Test()
        {
            // Arrange
            var mathinstanceObj = new MathInstance();

            // Act
            var result = mathinstanceObj.Max(1, -2);

            // Assert
            Assert.Equal(1, result);
        }
    }
}
