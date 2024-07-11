using Microsoft.EntityFrameworkCore;
using OnBoarding.api.Application.Responses;
using OnBoarding.api.Models;

namespace OnBoarding.api.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly OnboardingDBContext dBContext;
        private readonly ILogger<EmployeeRepository> logger;

        public EmployeeRepository(OnboardingDBContext dBContext, ILogger<EmployeeRepository> logger)
        {
            this.dBContext = dBContext;
            this.logger = logger;
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            return await dBContext.Employees.ToListAsync();
        }

        public async Task<Employee> Get(string email)
        {
            return await dBContext.Employees.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task Delete(string id)
        {
            var emp = await dBContext.Employees.FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));
            if (emp != null)
            {
                dBContext.Employees.Remove(emp);
                await dBContext.SaveChangesAsync();
            }
        }

        public async Task<Guid> AddEmployee(Employee employee)
        {
            var entity = dBContext.Employees.Add(employee);
            await dBContext.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<Guid> AddEmployee(string Firstname, string lastName, string email)
        {
            return await AddEmployee(new Employee() { FirstName = Firstname, LastName = lastName, Email = email });            
        }

        public async Task UpdateVerification(string email, bool status)
        {
            var emp = await Get(email);
            if(emp != null)
            {
                emp.VerificationDone = status;
                await dBContext.SaveChangesAsync();
            }
        }
    }
}
