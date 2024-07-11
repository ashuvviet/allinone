using BasicMocks;
using Core;
using Core.Contracts;
using NSubstitute;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace BasicMock_Tests
{
    [ExcludeFromCodeCoverage]
    public class TrainingControllerTests
    {
        [Fact]
        public async void TrainingController_Add_True_True_True_Tests()
        {
            // Arrange
            // naming service
            var namingService = Substitute.For<INamingService>();
            namingService.Validate(Arg.Any<string>()).Returns(true);
            Container.AddSingelten<INamingService>(namingService);

            // training DB context
            var mockTrainingData = Substitute.For<ITrainingData>();
            mockTrainingData.Add(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));

           
            // SMTP mail service
            var stmpMockObj = Substitute.For<IMailService>();
            stmpMockObj.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));
            Container.AddSingelten<IMailService>(stmpMockObj);

            var controller = new TrainingController(mockTrainingData);

            // Act
            var result = await controller.Add("SOLID", "200 $", "trainer@gmail.com");

            // Assert
            Assert.True(result);
            namingService.Received().Validate(Arg.Any<string>());
            await mockTrainingData.Received().Add(Arg.Any<string>(), Arg.Any<string>());
            await stmpMockObj.Received().SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async void TrainingController_Add_True_True_True2_Tests()
        {
            // Arrange
            // naming service
            var namingService = Substitute.For<INamingService>();
            namingService.Validate(Arg.Any<string>()).Returns(true);
            Container.AddSingelten<INamingService>(namingService);

            // training DB context
            bool addhappened = false;
            var mockTrainingData = Substitute.For<ITrainingData>();
            mockTrainingData.Add(Arg.Any<string>(), Arg.Any<string>()).Returns((c) =>
            {
                addhappened = true;
                return Task.FromResult(true);
            });


            // SMTP mail service
            var stmpMockObj = Substitute.For<IMailService>();
            stmpMockObj.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));
            Container.AddSingelten<IMailService>(stmpMockObj);

            var controller = new TrainingController(mockTrainingData);

            // Act
            var result = await controller.Add("SOLID", "200 $", "trainer@gmail.com");

            // Assert
            Assert.True(result);
            Assert.True(addhappened);
        }

        [Fact(Skip = "true")]
        public async void TrainingController_Add__True_True_False_Tests()
        {
            // Arrange
            var namingService = Substitute.For<INamingService>();
            namingService.Validate(Arg.Any<string>()).Returns(true);
            Container.AddSingelten<INamingService>(namingService);

            var mockTrainingData = Substitute.For<ITrainingData>();
            mockTrainingData.Add(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));

            var controller = new TrainingController(mockTrainingData);
            var stmpMockObj = Substitute.For<IMailService>();
            stmpMockObj.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(false));
            Container.AddSingelten<IMailService>(stmpMockObj);

            // Act
            var result = await controller.Add("SOLID", "200 $", "trainer@gmail.com");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void TrainingController_Add_True_False_False_Tests()
        {
            // Arrange
            var namingService = Substitute.For<INamingService>();
            namingService.Validate(Arg.Any<string>()).Returns(true);
            Container.AddSingelten<INamingService>(namingService);

            var mockTrainingData = Substitute.For<ITrainingData>();
            mockTrainingData.Add("SOLID", "200 $").Returns(Task.FromResult(false));
            var controller = new TrainingController(mockTrainingData);
            var stmpMockObj = Substitute.For<IMailService>();
            stmpMockObj.SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(false));
            Container.AddSingelten<IMailService>(stmpMockObj);

            // Act
            var result = await controller.Add("SOLID", "200 $", "trainer@gmail.com");

            // Assert
            var allTrainings = await controller.GetAllTrainings();
            //Assert.Single(allTrainings);
            Assert.False(result);
            await stmpMockObj.DidNotReceive().SendMail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void TrainingController_Add_False_XXX_Tests()
        {
            // Arrange
            var namingService = Substitute.For<INamingService>();
            namingService.Validate(Arg.Any<string>()).Returns(false);
            Container.AddSingelten<INamingService>(namingService);

            var mockTrainingData = Substitute.For<ITrainingData>();
            mockTrainingData.Add("SOLID", "200 $").Returns(Task.FromResult(false));
            var controller = new TrainingController(mockTrainingData);

            // Act
            Func<Task> result = async () => await controller.Add("SOLID", "200 $", "trainer@gmail.com");

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void TrainingController_Delete_Tests(bool expectedResult)
        {
            // Arrange      
            // training DB context
            var mockTrainingData = Substitute.For<ITrainingData>();
            mockTrainingData.Delete(Arg.Any<int>()).Returns(Task.FromResult(expectedResult));

            var controller = new TrainingController(mockTrainingData);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void TrainingMockVirtualMethodController_Delete_Tests(bool expectedResult)
        {
            // Arrange      
            // training DB context
            var mockTrainingData = Substitute.For<ITrainingData>();
            mockTrainingData.Delete(Arg.Any<int>()).Returns(Task.FromResult(expectedResult)); // NSub

            //mockTrainingData.Setup(s => s.Delete(It.IsAny<int>())).Returns(Task.FromResult(true)); // MoqQ

            //A.CallTo(() => mockTrainingData.Delete(A<int>._)).Returns(Task.FromResult(true)); //FakeItEasy


            var controller = Substitute.For<TrainingController>(mockTrainingData);
            //controller.ShowDelete(Arg.Any<string>());

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.Equal(expectedResult, result);
            controller.Received().ShowDelete(Arg.Any<string>());
            controller.ReceivedWithAnyArgs().ShowDelete("");
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void Training_Update_Tests(bool expectedResult)
        {
            // Arrange      
            var namingService = Substitute.For<INamingService>();
            namingService.Validate(Arg.Any<string>()).Returns(true);
            Container.AddSingelten<INamingService>(namingService);

            var mockTrainingData = Substitute.For<ITrainingData>();
            //mockTrainingData.Update(Arg.Any<int>(), Arg.Any<string>()).Returns(Task.FromResult(expectedResult));

            var controller = new TrainingController(mockTrainingData);

            // Act
            var result = await controller.Update(1, "test");

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TrainingById_Out_Params_Tests()
        {
            // Arrange
            var mockTrainingData = Substitute.For<ITrainingData>();
            //mockTrainingData.GetTranings(Arg.Any<int>(), out Arg.Any<Training>());
            mockTrainingData.When(s => s.GetTranings(Arg.Any<int>(), out Training t)).Do(x =>
            {
                x[1] = new Training();
            });

            var controller = new TrainingController(mockTrainingData);

            // Act
            controller.GetTrainings(1, out Training t);

            // Assert
            Assert.NotNull(t);
        }
    }
}
