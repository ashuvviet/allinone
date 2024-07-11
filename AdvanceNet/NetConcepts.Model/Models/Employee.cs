using NetConcepts.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetConcepts.Model.Models
{
    [MyEmployeeMetaDataAttibute(Order = 1, Tag ="Test")]
    internal class Employee
    {
        public string Name { get; }

        public Guid Id { get; }

        public int EmpCode { get; }

        public Employee(string name, int code)
        {
            Name = name;
            EmpCode = code;
            Id = Guid.NewGuid();
        }

        //public override string ToString()
        //{
        //    return string.Concat(EmpCode, ":", Name);
        //}
    }

    internal class FullTimeEmp : Employee 
    {
        public FullTimeEmp(string name, int code) : base (name, code)
        {

        }
    }

    internal class PartTimeEmp : Employee
    {
        public PartTimeEmp(string name, int code) : base(name, code)
        {

        }
    }

}
