using System;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Queuetrigger
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")]string myQueueItem, 
            [Blob("ashublob", Connection = "AzureWebJobsStorage")] CloudBlobContainer container, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference($"{myQueueItem}.txt");
            await blob.UploadTextAsync($"Created a new task: Welcome to azure {myQueueItem}");
            log.LogInformation($"C# Blob output processed: {myQueueItem}");
        }
    }
}
