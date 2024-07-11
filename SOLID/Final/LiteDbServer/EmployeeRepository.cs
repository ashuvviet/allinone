using Core.Contracts;
using Core.Models;
using DataBaseCore.DBContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseCore
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LiteDBContext employeeDBContext;

        public EmployeeRepository(LiteDBContext employeeDBContext)
        {
            this.employeeDBContext = employeeDBContext;
        }

        public async Task<Employee> Get(int id)
        {
            return await Task.FromResult(employeeDBContext.GetEmployeeById(id));
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await Task.FromResult(employeeDBContext.GetAllEmployees());
        }

        public Task<string> GetInsurance(int id)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetSalary(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertEmployee(Employee e)
        {
            return await Task.FromResult(employeeDBContext.InsertEmployee(e));
        }
    }
}
