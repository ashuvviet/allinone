using MediatR;
using Microsoft.AspNetCore.Mvc;
using moviebooking_api.DTOs;
using moviebooking_api.Model;

namespace moviebooking_api.Queries
{
    public class GetCinemasByMovieQuery : IRequest<IEnumerable<CinemaMovieDto>>
    {
        public GetCinemasByMovieQuery()
        {
        }

        public string MovieName { get; set; }

        public string City { get; set; }
    }
}