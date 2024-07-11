using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Contracts;

namespace BasicMocks
{
    public class TrainingController
    {
        private readonly ITrainingData _trainingData;

        public TrainingController(ITrainingData trainingData)
        {
            _trainingData = trainingData;
        }

        public async Task<IEnumerable<Training>> GetAllTrainings()
        {
            return await _trainingData.GetAllTrainings();
        }

        public void GetTrainings(int id, out Training t)
        {
             _trainingData.GetTranings(id, out t);
        }

        public async Task<bool> Add(string name, string cost, string mail)
        {
            // verification of inputs
            var namingService = Container.Resolve<INamingService>();
            if (!namingService.Validate(name))
            {
                throw new InvalidOperationException();
            }

            // adding into DB
            var success = await _trainingData.Add(name, cost);

            // sending mail
            if (success)
            {
                var smtpMailService = Container.Resolve<IMailService>();
                success = await smtpMailService.SendMail("test@gmail.com", mail, "welcome", "Welcome to .Net training");
            }

            return success;
        }

        public async Task<bool> Update(int id, string name)
        {
            // verification of inputs
            var namingService = Container.Resolve<INamingService>();
            if (!namingService.Validate(name))
            {
                throw new InvalidOperationException();
            }

            // updating into DB
            await _trainingData.Update(id, name);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException();
            }


            ShowDelete("before deleting Employee");
            var result = await _trainingData.Delete(id);
      
            ShowDelete("after deleting Employee");

            return result;
        }

        public virtual void ShowDelete(string msg)
        {
            Console.WriteLine("before deleting Employee");
        }

        //public virtual bool AfterDelete(string msg)
        //{
        //    Console.WriteLine("after deleting Employee");
        //}
    }
}
