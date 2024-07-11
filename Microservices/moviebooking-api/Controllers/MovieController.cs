using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using moviebooking_api.Commands;
using moviebooking_api.DTOs;
using moviebooking_api.Model;
using moviebooking_api.Queries;
using System.Data;

namespace moviebooking_api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MovieController(IMediator mediator, IMapper mapper, ILogger<MovieController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        //[Authorize(Roles = "SystemAdmin")]
        public async Task<ActionResult> Post(string movieName)
        {
            return Ok(await _mediator.Send(new CreateMasterMovieCommand() { MovieName = movieName }));
        }
    }
}