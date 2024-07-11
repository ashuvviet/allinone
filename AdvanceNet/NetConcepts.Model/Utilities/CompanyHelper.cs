using NetConcepts.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetConcepts.Model.Utilities
{
    internal static class CompanyHelper
    {
        public static async Task<Company> CreateCompany(string companyName, int noOfEmp)
        {
            var c = new Company(companyName);
            for (int i = 1; i < noOfEmp; i++)
            {
                if (i < noOfEmp / 2)
                {
                    c.Employees.Add(new FullTimeEmp($"Emp {i}", i));
                }
                else
                c.Employees.Add(new PartTimeEmp($"Emp {i}", i));
            }

            return await Task.FromResult(c);
        }       
    }
}
