using EmployeeManagementApi.Dto;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var l = await employeeRepository.GetAll();
            return Ok(l);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var e = await employeeRepository .Get(Id);
            return Ok(e);
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(EmployeeDto employeeDto)
        {
            if(string.IsNullOrEmpty(employeeDto.FirstName))
            {
                throw new InvalidOperationException();
            }

            var e = new Employee() { FirstName = employeeDto.FirstName, LastName = employeeDto.LastName };
            var result = await employeeRepository.InsertEmployee(e);
            //await employeeDBContext.SaveChangesAsync();

            // send mail to finance / insurance team
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult result = MessageBox.Show(message, title, buttons);

            return Ok(result);
        }
    }
}
