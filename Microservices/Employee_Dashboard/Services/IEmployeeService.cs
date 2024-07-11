using Employee_Dashboard.Model;

namespace Employee_Dashboard.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> Get();

        Task Create(string firstname, string lastName, string email);

        Task Delete(Guid id);
    }
}