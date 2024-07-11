// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

Console.WriteLine("Hello, World!");


var connectionString = "Endpoint=sb://demo8dec.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=4ivGTspnZTdjoZ3YpGjI2cmNOCZRnKA3R+ASbHN06uw=";

var queueName = "q1";


var client = new ServiceBusClient(connectionString);
var sender = client.CreateSender(queueName);


using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

for (int i = 0; i < 30; i++)
{
    var message = new ServiceBusMessage($"Mesage {i}");
    Console.WriteLine(message.ToString());
    if (!messageBatch.TryAddMessage(message))
    {
        throw new Exception($"The message {i} is too large to fit in the batch.");
    }
}

try
{
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine("A batch of 10 message published");
    Console.ReadKey();
}
catch (Exception)
{

	throw;
}
finally
{
    await  sender.DisposeAsync();
    await client.DisposeAsync();
}
