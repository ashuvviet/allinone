using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public interface IEmployee
    {
        string Email { get; set; }
        string FirstName { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
    }

    public interface IEmployeePayment
    {
        int BasicPay { get; set; }
        int Bonus { get; set; }
     
        int HRA { get; set; }
    
        int GetSalary();
    }

    public interface IInsurance
    {
        string GetInsurance();
    }

    public abstract class Employee : IEmployee, IEmployeePayment, IInsurance
    {
        /// <value>The id.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int BasicPay { get; set; }

        public int HRA { get; set; }

        public int Bonus { get; set; }

        public abstract int GetSalary();

        public virtual string GetInsurance()
        {
            return "Max +";
        }
    }

    public class FullTimeEmployee : Employee
    {
        public override int GetSalary()
        {
            return BasicPay + HRA + Bonus;
        }
    }

    public class PartTimeEmployee : Employee
    {
        public override int GetSalary()
        {
            return BasicPay + HRA;
        }

        public override string GetInsurance()
        {
            return "Max";
        }
    }
}
