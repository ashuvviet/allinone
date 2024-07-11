using MediatR;
using moviebooking_api.DTOs;

namespace moviebooking_api.Commands
{
    public class CreateCinemaCommand : IRequest<int>
    {
        public CinemaDto Cienema { get; set; }
    }
}