using Messaging.Core;

namespace OnBoarding.api.Application.Events
{
    public class AddNewEmplyeeEvent : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
