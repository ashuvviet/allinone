// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
const int numOfMessages = 10;

Console.WriteLine("Starting Publisher");

for (int i = 0; i < numOfMessages; i++)
{
    Thread.Sleep(2000);
    var m = new RMQMessage() { Message = $"My Message {i} from .net Producer console.", Index = i };
    Console.WriteLine(m.Message);
    Publish(m);   
}

Console.WriteLine("enter any key to stop publisher");
Console.ReadLine();

void Publish<T>(T @event)
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    var queueName = typeof(T).Name;

    channel.QueueDeclare(queue: queueName,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

    var message = Newtonsoft.Json.JsonConvert.SerializeObject(@event);
    var body = Encoding.UTF8.GetBytes(message);

    
    channel.BasicPublish("", queueName, null, body);
}


public class RMQMessage
{
    public string Message { get; set; }

    public int Index { get; set; }
}



