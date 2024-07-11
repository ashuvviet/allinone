using System;
using System.Collections.Generic;
using LiteDB;
using Microsoft.Extensions.Options;
using Core.Models;
using Core.Options;

namespace DataBaseCore.DBContext
{
    public class LiteDBContext : IDisposable
    {
        private static LiteDatabase _context;
        private static ILiteCollection<Employee> collection;
        private string nameOfCollection = "Employees";

        public LiteDBContext(IOptions<DbConfig> configs)
        {
            try
            {
                if (_context == null)
                {
                    _context = new LiteDatabase($"Filename={configs.Value.PathToDB};Connection=Direct");
                    collection = _context.GetCollection<Employee>(nameOfCollection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }
       
        public BsonValue InsertEmployee(Employee employee)
        {
            collection = _context.GetCollection<Employee>(nameOfCollection);
            employee.Id = collection.Count() + 1;
            var bsonValue = collection.Insert(employee);

            return bsonValue;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var Employees = collection.FindAll();

            return Employees;
        }

        public Employee GetEmployeeById(int EmployeeId)
        {
            var bs = new BsonValue(EmployeeId);

            var Employee = collection.FindById(bs);

            return Employee;
        }

        public bool UpdateEmployee(Employee Employee)
        {
            var hasUpdated = collection.Update(Employee);

            return hasUpdated;
        }

        public bool DeleteEmployeeById(int EmployeeId)
        {
            var bs = new BsonValue(EmployeeId);

            var hasDeleted = collection.Delete(bs);

            return hasDeleted;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
