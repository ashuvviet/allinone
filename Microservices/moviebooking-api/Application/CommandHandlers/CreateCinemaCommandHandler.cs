using AutoMapper;
using MediatR;
using moviebooking_api.Application.QueryHandlers;
using moviebooking_api.Commands;
using moviebooking_api.Repositories;

namespace moviebooking_api.Application.CommandHandlers
{
    public class CreateCinemaCommandHandler : IRequestHandler<CreateCinemaCommand, int>
    {
        private readonly ILogger<CreateCinemaCommandHandler> _logger;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;

        public CreateCinemaCommandHandler(ILogger<CreateCinemaCommandHandler> logger,
            ICinemaRepository cinemaRepository)
        {
            _logger = logger;
            _cinemaRepository = cinemaRepository;
        }

        public async Task<int> Handle(CreateCinemaCommand request, CancellationToken cancellationToken)
        {
            await _cinemaRepository.AddCinema(request.Cienema);
            return 1;
        }
    }
}
