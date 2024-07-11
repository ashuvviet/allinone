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
    public class CinemaController : ControllerBase
    {
        private readonly ILogger<CinemaController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CinemaController(IMediator mediator, IMapper mapper, ILogger<CinemaController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost]
        //[Authorize(Roles = "SystemAdmin")]
        public async Task<ActionResult> Post([FromBody] CinemaDto cinema)
        {
            return Ok(await _mediator.Send(new CreateCinemaCommand() { Cienema = cinema }));
        }       
    }
}