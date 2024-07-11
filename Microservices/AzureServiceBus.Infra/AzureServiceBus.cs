using Messaging.Core;

namespace AzureServiceBus.Infra
{
    public class AzureServiceBus : IEventBus
    {
        public void Publish<T>(T @event) where T : Event
        {
            
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
           
        }
    }
}