using Core.Contracts;
using Core.Models;
using EmployeeManagementApi.Dto;
using EmployeeWebApi.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMailService _mailService;
        private readonly INamingService _namingService;
        private readonly IMediator _mediator;

        public EmployeeController(ILogger<EmployeeController> logger,
            IEmployeeRepository employeeRepository,
            IMailService mailService, INamingService namingService, IMediator mediator)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mailService = mailService;
            _namingService = namingService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetEmployee()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return Ok(await _employeeRepository.Get(id));
        }

        [HttpGet("insurance")]
        public async Task<IActionResult> GetEmployeeInsurance(int id)
        {
            return Ok(await _employeeRepository.GetInsurance(id));
        }

        [HttpGet("salary")]
        public async Task<IActionResult> GetEmployeeSalary(int id)
        {
            return Ok(await _employeeRepository.GetSalary(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(EmployeeDto employeeDto)
        {
            if (!_namingService.IsValid(employeeDto.FirstName) || !_namingService.IsValid(employeeDto.LastName))
            {
                throw new InvalidOperationException();
            }

            if (!_mailService.IsValid(employeeDto.Email))
            {
                throw new InvalidOperationException();
            }

            var e = new FullTimeEmployee() { FirstName = employeeDto.FirstName, LastName = employeeDto.LastName, Email = employeeDto.Email };
            var result = await _employeeRepository.InsertEmployee(e);

            // send mail to employee
            await _mailService.SendMail(e.Email, "Welcome Mail", "Welcome to Danaher");

            return Ok(result);
        }

        [HttpPost]
        [Route("parttime")]
        public async Task<IActionResult> InsertPartTimeEmployee(EmployeeDto employeeDto)
        {
            if (!_namingService.IsValid(employeeDto.FirstName) || !_namingService.IsValid(employeeDto.LastName))
            {
                throw new InvalidOperationException();
            }

            if (!_mailService.IsValid(employeeDto.Email))
            {
                throw new InvalidOperationException();
            }

            var e = new PartTimeEmployee() { FirstName = employeeDto.FirstName, LastName = employeeDto.LastName, Email = employeeDto.Email };
            var result = await _employeeRepository.InsertEmployee(e);

            // send mail to employee
            await _mailService.SendMail(e.Email, "Welcome", "Welcome To xyz");

            return Ok(result);
        }
    }
}
