using Core;
using Core.Models;
using DataBaseCore.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseCore
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext employeeDBContext;

        public EmployeeRepository(EmployeeDBContext employeeDBContext)
        {
            this.employeeDBContext = employeeDBContext;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeeDBContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await employeeDBContext.Employees.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Employee> GetByName(string name)
        {
            return await employeeDBContext.Employees.FirstOrDefaultAsync(s => s.FirstName == name);
        }

        public async Task<int> GetSalary(int id)
        {
            var emp = await GetById(id);
            return emp.GetSalary();
        }

        public async Task<string> GetInsurance(int id)
        {
            var emp = await GetById(id);
            return emp.GetInsurance();
        }

        public async Task<int> InsertEmployee(Employee employee)
        {
           employeeDBContext.Employees.Add(employee);
           return await employeeDBContext.SaveChangesAsync();
        }
    }
}
