using BasicModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTest_BasicModules_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod("Add_MSTest_Add_Test")]
        public void Add_Test()
        {
            // Arrange
            var mathinstanceObj = new MathInstance();

            // Act
            var result = mathinstanceObj.Add(1, 2);

            // Assert
            Assert.AreEqual(3, result);
        }
    }
}
