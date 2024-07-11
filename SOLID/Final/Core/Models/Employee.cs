using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public abstract class Employee : IEmployee, IPayment, IInsurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int BasicPay { get; set; }

        public int HRA { get; set; }

        public int Bonus { get; set; }

        public abstract string GetInsurance();

        public abstract long GetSalary();
    }

    public class FullTimeEmployee : Employee
    {
        public override string GetInsurance()
        {
            return "Adtiya Birla";
        }

        public override long GetSalary()
        {
            return BasicPay + HRA + Bonus;
        }
    }

    public class PartTimeEmployee : Employee
    {
        public override string GetInsurance()
        {
            return "Max";
        }

        public override long GetSalary()
        {
            return BasicPay + HRA;
        }
    }
}
