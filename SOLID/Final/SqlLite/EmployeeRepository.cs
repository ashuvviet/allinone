using Core.Contracts;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlLite
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<Employee> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetInsurance(int id)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetSalary(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
