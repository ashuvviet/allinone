using System;

namespace BasicModule.BasicClasses
{
    internal class Employee
    {
        public string Name { get; }

        public Guid Id { get; }

        public Employee(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }

    internal class FullTimeEmp : Employee
    {
        public FullTimeEmp(string name) : base(name)
        {

        }
    }

    internal class PartTimeEmp : Employee
    {
        public PartTimeEmp(string name) : base(name)
        {

        }
    }
}
