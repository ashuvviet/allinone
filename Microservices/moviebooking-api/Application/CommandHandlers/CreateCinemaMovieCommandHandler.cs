using AutoMapper;
using MediatR;
using moviebooking_api.Application.QueryHandlers;
using moviebooking_api.Commands;
using moviebooking_api.Repositories;

namespace moviebooking_api.Application.CommandHandlers
{
    public class CreateCinemaMovieCommandHandler : IRequestHandler<CreateCinemaMovieCommand, int>
    {
        private readonly ILogger<CreateCinemaMovieCommandHandler> _logger;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;

        public CreateCinemaMovieCommandHandler(ILogger<CreateCinemaMovieCommandHandler> logger,
            ICinemaRepository cinemaRepository)
        {
            _logger = logger;
            _cinemaRepository = cinemaRepository;
        }

        public async Task<int> Handle(CreateCinemaMovieCommand request, CancellationToken cancellationToken)
        {
            await _cinemaRepository.AddCinemaMovie(request.Movie);
            return 1;
        }
    }
}
