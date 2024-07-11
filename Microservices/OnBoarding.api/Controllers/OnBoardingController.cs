using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnBoarding.api.Application.Commands;
using OnBoarding.api.Application.Queries;
using OnBoarding.api.Dto;
using OnBoarding.api.Filter;
using OnBoarding.api.Models;
using OnBoarding.api.Options;

namespace OnBoarding.api.Controllers
{
    [ApiController]
    //[ServiceFilter(typeof(EmployeeFilter))]
    //[ApiVersion("1.0")]
    //[Route("api/{v:apiversion}/[controller]")]
    [Route("[controller]/api/v1")]
    public class OnBoardingController : ControllerBase
    {
        private readonly ILogger<OnBoardingController> _logger;
        private readonly IMediator _mediator;

        public OnBoardingController(ILogger<OnBoardingController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// to get all employees
        /// </summary>
        /// <returns></returns>
        //ODATA - https://localhost:5012/onboarding/$filter = department eq 'IT' and firstname eq 'John' and lastname eq 'Doe'
        [HttpGet]
        //[EnableQuery]
        public async Task<IActionResult> Get() => await _mediator.Send(new GetEmployeeQuery());

        //[HttpGet]
        //[Route("api/v2")]
        ////[EnableQuery]
        //public async Task<IActionResult> GetV2() => await _mediator.Send(new GetEmployeeQuery());


        //// https://localhost:5012/onboarding/{id}
        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> GetById(int id) => await _mediator.Send(new GetEmployeeQuery());

        //// https://localhost:5012/onboarding/department
        //[HttpGet]
        //[Route("department")]
        //public async Task<IActionResult> GetByDepartment(string department) => await _mediator.Send(new GetEmployeeQuery());

        //// https://localhost:5012/onboarding/firstname
        //[HttpGet]
        //[Route("firstname")]
        //public async Task<IActionResult> GetByFirstName(string firstname) => await _mediator.Send(new GetEmployeeQuery());

        //// https://localhost:5012/onboarding/lastname
        //[HttpGet]
        //[Route("lastname")]
        //public async Task<IActionResult> GetByLastname(string lastname) => await _mediator.Send(new GetEmployeeQuery());

        // ODATA


        /// <summary>
        /// to Insert new Employee
        /// </summary>
        /// <param name="employee">empoyee object</param>
        /// <returns></returns>
        [HttpPost]
      
        public async Task<IActionResult> Post([FromBody] EmployeeDto employee)
        {
            return  await _mediator.Send(new CreateEmployeeCommand() { Employee = employee });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return await _mediator.Send(new DeleteEmployeeCommand() { Id = id });
        }

        //[HttpPut]
        //public async Task<IActionResult> Put([FromBody]EmployeeDto employee) =>
        //    await _mediator.Send(new UpdateEmployeeCommand() { Employee = employee });

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody]EmployeeDto employee) =>
        //    await _mediator.Send(new DeleteEmployeeCommand() { Employee = employee });

        // https://localhost:5012/onboarding [Get, Post, Put, delete]
        // https://localhost:5012/onboarding/{id}

    }
}