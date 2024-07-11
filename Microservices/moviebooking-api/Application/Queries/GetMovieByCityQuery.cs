using MediatR;
using Microsoft.AspNetCore.Mvc;
using moviebooking_api.DTOs;
using moviebooking_api.Model;

namespace moviebooking_api.Queries
{
    public class GetMovieByCityQuery : IRequest<IEnumerable<MovieDto>>
    {
        public GetMovieByCityQuery()
        {
        }

        public string City { get; set; }
    }
}