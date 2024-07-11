using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(int id);
        Task<IEnumerable<Employee>> GetAll();
        Task<int> InsertEmployee(Employee e);
    }

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

        public async Task<int> InsertEmployee(Employee e)
        {
            return await Task.FromResult(employeeDBContext.InsertEmployee(e));
        }
    }
}
