﻿using Core.Models;
using Microsoft.EntityFrameworkCore;
using DataBaseCore.DBContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

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
            return await employeeDBContext.Employees.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeeDBContext.Employees.ToListAsync();
        }

        public Task<Employee> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> GetByName(string name)
        {
            throw new System.NotImplementedException();
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
            employeeDBContext.Employees.Add(e);
            return await employeeDBContext.SaveChangesAsync();
        }

        Task<int> IEmployeeRepository.GetSalary(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
