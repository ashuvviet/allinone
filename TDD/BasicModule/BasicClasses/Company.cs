using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("XUnit_BasicModules_Tests")]
namespace BasicModule.BasicClasses
{
    internal class Company
    {
        public IList<Employee> Employees;

        public string Name { get; set; }

        public Company(string name)
        {
            Name = name;
            Employees = new List<Employee>();
            Employees.Add(new Employee("First Emp"));
        }

        public Employee this[string name]
        {
            get
            {
                return Employees.FirstOrDefault(s => s.Name == name);
            }
        }

        public void Add(Employee e)
        {
            if(string.IsNullOrEmpty(e.Name))
            {
                throw new InvalidOperationException();
            }

            Employees.Add(e);
        }

        public void Remove(string name)
        {
            var e = Employees.FirstOrDefault(s => s.Name == name);
            if (e == null)
            {
                throw new InvalidOperationException();
            }

            Employees.Remove(e);
        }

    }
}
