using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IEmployeeRepository
    {
        Task<int> InsertEmployee(Employee employee);

        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> GetByName(string name);

        Task<Employee> GetById(int id);

        Task<int> GetSalary(int id);

        Task<string> GetInsurance(int id);
    }
}
