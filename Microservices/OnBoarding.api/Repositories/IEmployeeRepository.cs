using OnBoarding.api.Application.Responses;
using OnBoarding.api.Models;

namespace OnBoarding.api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Guid> AddEmployee(Employee employee);
        Task<Guid> AddEmployee(string Firstname, string lastName, string email);
        Task<IEnumerable<Employee>> Get();
        Task<Employee> Get(string email);
        Task UpdateVerification(string email, bool status);

        Task Delete(string id);
    }
}