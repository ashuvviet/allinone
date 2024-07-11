using MediatR;
using moviebooking_api.DTOs;

namespace moviebooking_api.Commands
{
    public class CreateCinemaMovieCommand : IRequest<int>
    {
        public MovieDto Movie { get; set; }
    }
}