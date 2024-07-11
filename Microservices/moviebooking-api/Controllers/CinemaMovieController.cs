using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using moviebooking_api.Commands;
using moviebooking_api.DTOs;
using moviebooking_api.Queries;

namespace moviebooking_api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class CinemaMovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CinemaMovieController(IMediator mediator, IMapper mapper, ILogger<MovieController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("{ownerId}/modify")]
        //[Authorize(Roles = "CinemaAdmin")]
        public async Task<ActionResult> Put(string ownerId, [FromBody] MovieDto movieDto)
        {
            var userContextDetails = GetConextOwner();
            if (userContextDetails != ownerId)
            {
                return new UnauthorizedResult();
            }

            await Task.CompletedTask;
            return Ok();
            //return Ok(await _mediator.Send(new UpdateMovieShowTimingCommand() { Movie = movieDto }));
        }

        [HttpPost]
        //[Authorize(Roles = "CinemaAdmin")]
        public async Task<ActionResult> Post([FromBody] MovieDto movieDto)
        {
            //var userContextDetails = GetConextOwner();
            //movieDto.Cinema.OwnerId = userContextDetails;
            try
            {
                return Ok(await _mediator.Send(new CreateCinemaMovieCommand() { Movie = movieDto }));
            }
            catch (UnauthorizedAccessException)
            {
                return new UnauthorizedResult();
            }
        }

        [HttpGet]
        [Route("moviebycity")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult> Get(string city)
        {
            return Ok(await _mediator.Send(new GetMovieByCityQuery() { City = city }));
        }

        [HttpGet]
        [Route("cinemabymovie")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult> Get(string movieName, string city)
        {
            return Ok(await _mediator.Send(new GetCinemasByMovieQuery() { MovieName = movieName, City = city }));
        }          

        [NonAction]
        public string GetConextOwner()
        {
            return HttpContext.User.Claims.FirstOrDefault(x => x.Type == "OwnerId")?.Value;           
        }
    }
}