using Core.Contracts;
using Core.Models;
using DataBaseCore.DBContext;
using System.Collections.Generic;
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

        public async Task<Employee> Get(int id)
        {
            return await employeeDBContext.GetEmployeeById(id);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeeDBContext.Employees();
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
    }
}
