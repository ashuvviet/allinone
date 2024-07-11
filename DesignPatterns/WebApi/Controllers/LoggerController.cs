using Customers.Api.Application.Commands;
using Customers.Api.Application.Queries;
using Customers.Domain.Models;
using Customers.Domain.Repositories;
using Customers.Infra.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggerController : ControllerBase
    {
        private readonly ILogger<LoggerController> _logger;
        private readonly ILoggerDBContext loggerDBContext;

        public LoggerController(ILogger<LoggerController> logger, ILoggerDBContext loggerDBContext)
        {
            _logger = logger;
            this.loggerDBContext = loggerDBContext;
        }

        [HttpGet]
        public IActionResult GetAllLogs()
        {
            var logs = loggerDBContext.GetAllLogs();
            return Ok(logs);
        }
    }
}
