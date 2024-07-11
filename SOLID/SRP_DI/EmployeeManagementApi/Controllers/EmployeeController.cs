using EmployeeManagementApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MediatR;
using EmployeeManagementApi.Application.Commands;
using EmployeeManagementApi.Application.Query;

namespace EmployeeManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
            //this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllEmployeeQuery()));

        [HttpGet]
        [Route("firstname")]
        public async Task<IActionResult> GetByName(string firstName) => Ok(await _mediator.Send(new GetEmployeeByNameQuery() { Name = firstName }));

        //[HttpGet]
        //[Route("fullsalary")]
        //public async Task<IActionResult> GetSalary(int id)
        //{
        //    if (!_namingService.IsValid(id))
        //    {
        //        throw new InvalidOperationException();
        //    }

        //    var result = await _employeeRepository.GetSalary(id);
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("insurance")]
        //public async Task<IActionResult> GetInsurance(int id)
        //{
        //    if (!_namingService.IsValid(id))
        //    {
        //        throw new InvalidOperationException();
        //    }

        //    var result = await _employeeRepository.GetInsurance(id);
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<ActionResult> InsertEmployee(EmployeeDto employeeDto) => 
         Ok(await _mediator.Send(new InsertEmployeCommand { FirstName = employeeDto.FirstName, LastName = employeeDto.LastName, Email = employeeDto.Email }));
    
    }   
}
