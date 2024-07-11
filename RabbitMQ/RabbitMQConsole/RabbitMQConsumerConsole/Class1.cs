//// See https://aka.ms/new-console-template for more information
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;

//Console.WriteLine("Starting Consumer");
//StartBasicConsume<RMQMessage>();
//Console.WriteLine("enter any key to stop consumer");
//Console.ReadLine();

//void StartBasicConsume<T>() where T : RMQMessage
//{
//    var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
//    using var connection = factory.CreateConnection();
//    using var channel = connection.CreateModel();

//    var queueName = typeof(T).Name;

//    channel.QueueDeclare(queue: queueName,
//                               durable: true,
//                               exclusive: false,
//                               autoDelete: false,
//                               arguments: null);

//    var consumer = new AsyncEventingBasicConsumer(channel);
//    consumer.Received += Consumer_Received;

//    channel.BasicConsume(queue: queueName,
//                            autoAck: true,
//                            consumer: consumer);
//}

//async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
//{
//    var eventName = @event.RoutingKey;
//    var message = Encoding.UTF8.GetString(@event.Body.ToArray());

//    var rmqMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<RMQMessage>(message);
//    Console.WriteLine($"Received message: {rmqMessage.Message} with index {rmqMessage.Index}");
//    await Task.CompletedTask;
//}


//public class RMQMessage
//{
//    public string Message { get; set; }

//    public int Index { get; set; }
//}

