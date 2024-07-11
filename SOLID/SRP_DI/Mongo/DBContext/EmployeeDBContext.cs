using Core.Models;
using Microsoft.Extensions.Options;
using Core.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DataBaseCore.DBContext
{
    public class EmployeeDBContext
    {
        private IMongoCollection<FullTimeEmployee> _fullemployeeCollection;
        private IMongoCollection<PartTimeEmployee> _partemployeeCollection;

        public EmployeeDBContext(IOptions<DbConfig> dbOptions)
        {
            var mongoDbConfig = dbOptions.Value.PathToDB;
            try
            {
                var databaseName = MongoUrl.Create(mongoDbConfig)?.DatabaseName;
                var dbClient = new MongoClient(mongoDbConfig);
                var db = dbClient.GetDatabase(databaseName);

                _fullemployeeCollection = db.GetCollection<FullTimeEmployee>("FullTimeEmployees");
                _partemployeeCollection = db.GetCollection<PartTimeEmployee>("PartTimeEmployees");
            }
            catch (MongoConfigurationException)
            {
                throw new Exception("Invalid mongo configuration or ConnectionString!");
            }
        }

        public async Task<IEnumerable<Employee>> Employees()
        {
            var list = new List<Employee>();
            var fullemployess = await _fullemployeeCollection.Find(_ => true).ToListAsync();
            var partemployess = await _partemployeeCollection.Find(_ => true).ToListAsync();
            list.AddRange(fullemployess);
            list.AddRange(partemployess);
            return list;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee emp = await GetFullEmployeeById(id);
            if (emp == null)
            {
                emp = await GetPartEmployeeById(id);
            }

            return emp;
        }

        private async Task<FullTimeEmployee> GetFullEmployeeById(int id) => await _fullemployeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        private async Task<PartTimeEmployee> GetPartEmployeeById(int id) => await _partemployeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task InsertAsync(Employee newemployee)
        {
            if (newemployee is FullTimeEmployee timeEmployee)
            {
                await InsertAsync(timeEmployee);
                return;
            }

            await InsertAsync(newemployee as PartTimeEmployee);
            return;
        }

        private async Task InsertAsync(FullTimeEmployee newemployee)
        {
            var employees = await Employees();
            newemployee.Id = employees.Count() + 1;
            var employee = await GetFullEmployeeById(newemployee.Id);
            if (employee == null)
            {
                await _fullemployeeCollection.InsertOneAsync(newemployee);
            }
            else
            {
                await UpdateAsync(employee);
            }
        }

        private async Task InsertAsync(PartTimeEmployee newemployee)
        {
            var employees = await Employees();
            newemployee.Id = employees.Count() + 1;
            var employee = await GetPartEmployeeById(newemployee.Id);
            if (employee == null)
            {
                await _partemployeeCollection.InsertOneAsync(newemployee);
            }
            else
            {
                await UpdateAsync(employee);
            }
        }

        private async Task UpdateAsync(FullTimeEmployee employee)
        {
            await _fullemployeeCollection.ReplaceOneAsync(x => x.Id == employee.Id, employee);
        }

        private async Task UpdateAsync(PartTimeEmployee employee)
        {
            await _partemployeeCollection.ReplaceOneAsync(x => x.Id == employee.Id, employee);
        }
    }
}
