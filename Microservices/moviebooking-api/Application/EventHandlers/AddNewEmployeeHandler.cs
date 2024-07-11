using Messaging.Core;
using moviebooking_api.Application.Events;
using moviebooking_api.Repositories;

namespace moviebooking_api.Application.EventHandlers
{
    public class AddNewEmployeeHandler : IEventHandler<AddNewEmplyeeEvent>
    {
        public AddNewEmployeeHandler(ICinemaRepository cinemaRepository) { 

        }

        public Task Handle(AddNewEmplyeeEvent @event)
        {
            
            // Assign new Movie ticket pass to New Employee

            return Task.CompletedTask;

        }
    }

    public class AddNewDummyEmployeeHandler : IEventHandler<AddNewEmplyeeEvent>
    {
        public AddNewDummyEmployeeHandler(ICinemaRepository cinemaRepository)
        {

        }

        public Task Handle(AddNewEmplyeeEvent @event)
        {

            // Assign new Movie ticket pass to New Employee

            return Task.CompletedTask;

        }
    }
}
