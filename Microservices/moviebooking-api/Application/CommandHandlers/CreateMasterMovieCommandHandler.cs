using AutoMapper;
using MediatR;
using moviebooking_api.Application.QueryHandlers;
using moviebooking_api.Commands;
using moviebooking_api.Repositories;

namespace moviebooking_api.Application.CommandHandlers
{
    public class CreateMasterMovieCommandHandler : IRequestHandler<CreateMasterMovieCommand, int>
    {
        private readonly ILogger<CreateMasterMovieCommandHandler> _logger;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;

        public CreateMasterMovieCommandHandler(ILogger<CreateMasterMovieCommandHandler> logger,
            ICinemaRepository cinemaRepository)
        {
            _logger = logger;
            _cinemaRepository = cinemaRepository;
        }

        public async Task<int> Handle(CreateMasterMovieCommand request, CancellationToken cancellationToken)
        {
            await _cinemaRepository.AddMovie(new DTOs.MovieDto() { Name = request.MovieName });
            return 1;
        }
    }
}
