using BasicMocks;
using Core;
using Core.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Training.Api.FuncTests
{
    public class TrainingControllerFuncTests
    {
        [Fact]
        public async void TrainingController_GetTrainings()
        {
            // Arrange
            var conext = new TrainingDataContext();
            var controller = new TrainingController(conext);

            // act
            var result = await controller.GetAllTrainings();

            // assert
            Assert.Single(result);
        }

        [Fact]
        public async void TrainingController_AddTrainings()
        {
            // Arrange
            var conext = new TrainingDataContext();
            var controller = new TrainingController(conext);
            Container.AddSingelten<INamingService>(new NamingService());
            Container.AddSingelten<IMailService>(new MockMailService());
            var beforelistofTrainings = await controller.GetAllTrainings();
            var beforecount = beforelistofTrainings.Count();

            // act
            var result = await controller.Add("C#", "100 $", "test@gmail.com");
            var listofTrainings = await controller.GetAllTrainings();

            // assert
            Assert.True(result);
            Assert.Equal(beforecount + 1, listofTrainings.Count());
        }
    }

    public class MockMailService : IMailService
    {
        public string Name => throw new NotImplementedException();

        public async Task<bool> SendMail(string sender, string target, string subject, string body)
        {
            return await Task.FromResult(true);
        }
    }
}
