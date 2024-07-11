using AutoMapper;
using MediatR;
using moviebooking_api.Controllers;
using moviebooking_api.DTOs;
using moviebooking_api.Model;
using moviebooking_api.Queries;
using moviebooking_api.Repositories;

namespace moviebooking_api.Application.QueryHandlers
{
    internal class GetMovieByCityQueryHandler : IRequestHandler<GetMovieByCityQuery, IEnumerable<MovieDto>>
    {

        private readonly ILogger<GetMovieByCityQueryHandler> _logger;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;

        public GetMovieByCityQueryHandler(ILogger<GetMovieByCityQueryHandler> logger,
            ICinemaRepository cinemaRepository)
        {
            _logger = logger;
            _cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetMovieByCityQuery request, CancellationToken cancellationToken)
        {
            return await _cinemaRepository.GetCinemaMovieByCityAsync(request.City);
        }
    }
}
