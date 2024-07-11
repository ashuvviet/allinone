

using Azure.Messaging.ServiceBus;

var connectionString = "Endpoint=sb://demo8dec.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=4ivGTspnZTdjoZ3YpGjI2cmNOCZRnKA3R+ASbHN06uw=";

var queueName = "q1";


var client = new ServiceBusClient(connectionString);
var processor = client.CreateProcessor(queueName);

processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

await processor.StartProcessingAsync();
Console.WriteLine("Wait for a min and then press key to end processing");

Console.ReadKey();  

await processor.StopProcessingAsync();

await processor.DisposeAsync();
await client.DisposeAsync();



Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}

async Task MessageHandler(ProcessMessageEventArgs args)
{
    var body = args.Message.Body.ToString();

    Console.WriteLine($"Received: {body}");

    await args.CompleteMessageAsync(args.Message);
}