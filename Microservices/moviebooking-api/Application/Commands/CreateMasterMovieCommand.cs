using MediatR;
using moviebooking_api.DTOs;

namespace moviebooking_api.Commands
{
    public class CreateMasterMovieCommand : IRequest<int>
    {
        public string MovieName { get; set; }
    }
}