using Core;
using Core.Models;
using DataBaseCore.DBContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseCore.Extensions
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext employeeDBContext;

        public EmployeeRepository(EmployeeDBContext employeeDBContext)
        {
            this.employeeDBContext = employeeDBContext;
        }

        public async Task<Employee> Get(int id)
        {
            return await employeeDBContext.GetEmployeeById(id);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeeDBContext.Employees();
        }

        public Task<Employee> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetInsurance(int id)
        {
            var e = await Get(id);
            return e.GetInsurance();
        }

        public async Task<long> GetSalary(int id)
        {
            var e = await Get(id);
            return e.GetSalary();
        }

        public async Task<int> InsertEmployee(Employee e)
        {
            await employeeDBContext.InsertAsync(e);
            return 1;
        }

        Task<int> IEmployeeRepository.GetSalary(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}