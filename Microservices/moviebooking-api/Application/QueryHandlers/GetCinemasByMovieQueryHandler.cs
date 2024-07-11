using AutoMapper;
using MediatR;
using moviebooking_api.Controllers;
using moviebooking_api.DTOs;
using moviebooking_api.Model;
using moviebooking_api.Queries;
using moviebooking_api.Repositories;

namespace moviebooking_api.Application.QueryHandlers
{
    internal class GetCinemasByMovieQueryHandler : IRequestHandler<GetCinemasByMovieQuery, IEnumerable<CinemaMovieDto>>
    {

        private readonly ILogger<GetCinemasByMovieQueryHandler> _logger;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;

        public GetCinemasByMovieQueryHandler(ILogger<GetCinemasByMovieQueryHandler> logger,
            ICinemaRepository cinemaRepository)
        {
            _logger = logger;
            _cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaMovieDto>> Handle(GetCinemasByMovieQuery request, CancellationToken cancellationToken)
        {
            return await _cinemaRepository.GetCinemaByMovieAsync(request.MovieName, request.City);
        }
    }
}
