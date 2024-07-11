using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IEmployeeRepository
    {
        Task<int> InsertEmployee(Employee employee);

        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> Get(int id);

        Task<long> GetSalary(int id);

        Task<string> GetInsurance(int id);
    }
}
