using Customers.Api.Application.Commands;
using Customers.Api.Application.Queries;
using Customers.Domain.Models;
using Customers.Domain.Repositories;
using Customers.Infra.Helpers;
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
    public class CustomerController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IMediator _mediator;

        public CustomerController(ILoggerService logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            _logger.Log("Getting all customers...");
            var getCustomersQuery = new GetCustomersQuery();
            var customers = _mediator.Send(getCustomersQuery);
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            _logger.Log("Getting customer by id...");
            var customer = _mediator.Send(new GetCustomerByIdQuery() { CustomerId = customerId });
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomer(Customer customer)
        {
            _logger.Log("Inserting customer...");
            var createCustomerCommand = new CreateCustomerCommand() { City = customer.City, Id = customer.Id, CompanyName = customer.CompanyName, Name = customer.Name, PhoneNumber = customer.PhoneNumber };
            var customers = await _mediator.Send(createCustomerCommand);
            return Ok(customers.Id);
        }

        //[HttpPut]
        //public IActionResult UpdateCustomer(Customer customer)
        //{
        //    _logger.LogInformation("Updating customer...");

        //    var hasUpdated = _dbServices.UpdateCustomer(customer);

        //    return Ok();
        //}

        //[HttpDelete("{customerId}")]
        //public IActionResult DeleteCustomer(int customerId)
        //{
        //    _logger.LogInformation("Deleting customer by id...");

        //    var hasUpdated = _dbServices.DeleteCustomerById(customerId);

        //    return Ok();
        //}
    }
}
