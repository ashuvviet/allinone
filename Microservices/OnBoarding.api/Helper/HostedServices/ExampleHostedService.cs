using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using MediatR;
using Newtonsoft.Json;
using OnBoarding.api.Application.Commands;
using OnBoarding.api.Application.Queries;
using Polly.Registry;
using Polly.Retry;
using System.ComponentModel.DataAnnotations;

namespace OnBoarding.api.Helper.HostedServices
{
    public class ExampleHostedService : BackgroundService
    {
        private readonly ILogger<ExampleHostedService> logger;
        private readonly IMediator _mediator;
        private readonly IReadOnlyPolicyRegistry<string> policyRegistry;
        private readonly AmazonSQSClient _sqsClient;
        private readonly IIncrementService incrementService;
        private int executionCount = 0;
        private const string queueUrl = "";

        public ExampleHostedService(ILogger<ExampleHostedService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            _sqsClient = new AmazonSQSClient("", "", RegionEndpoint.USEast2);
            var scope = serviceProvider.CreateScope();            
            this.incrementService = scope.ServiceProvider.GetRequiredService<IIncrementService>();
            this._mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            this.policyRegistry = scope.ServiceProvider.GetRequiredService<IReadOnlyPolicyRegistry<string>>();

        }

        private void IncrementTask(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //1. create rabbitmQ channel and channel will be pooling the message from queue.

                // if(message.any())
                // 2. once message is received, then call command/event using CQRS -> Imediator


                // 3. mediator.Send(Save Movie)


                // 4. go and acknowledge the mssage.
                var count = incrementService.Increment(ref executionCount);
                //Thread.Sleep(5 * 1000);
                logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
            }            
        }

        private async Task ProcessSQS(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var retryPolicy = policyRegistry.Get<AsyncRetryPolicy>("sqsretrypolicy");

                var policyResponse = await retryPolicy.ExecuteAndCaptureAsync(async () =>
                {
                    // 1. pull the message fro queue
                    var response = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest() { QueueUrl = queueUrl }, stoppingToken);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // 2.Process
                        if (response != null && response.Messages.Count > 0)
                        {
                            var message = response.Messages[0];
                            var result = JsonConvert.DeserializeObject<EmployeeValidationDetails>(message.Body);

                            await _mediator.Send(new UpdateEmpVerificationCommand() { Message = result });

                            // 3. Delete
                            await _sqsClient.DeleteMessageAsync(new DeleteMessageRequest() { QueueUrl = queueUrl, ReceiptHandle = message.ReceiptHandle });

                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                });            
               
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Task.Run(() => { IncrementTask(stoppingToken); });
            //Task.Run(async () => { await ProcessSQS(stoppingToken); });
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }


    public class EmployeeValidationDetails
    {
        public string Email { get; set; }

        public bool Status { get; set; }

        public string Message { get; set; }
    }

    public interface IIncrementService
    {
        int Increment(ref int count);
    }

    public class IncrementService : IIncrementService
    {
        public int Increment(ref int count)
        {
            return Interlocked.Increment(ref count);
        }
    }
}
