namespace Messaging.Core
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;

    }

    public interface IEventHandler
    {
        
    }

    public interface IEventHandler<in T> : IEventHandler where T : Event
    {
        Task Handle(T @event);
    }

    public abstract class Event
    {
        public DateTime Timestamp { get; protected set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}