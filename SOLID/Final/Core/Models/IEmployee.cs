namespace Core.Models
{
    public interface IEmployee
    {
        int Id { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }      
        string LastName { get; set; }      
    }

    public interface IPayment
    {
        int BasicPay { get; set; }
        int Bonus { get; set; }
        int HRA { get; set; }
        long GetSalary();
    }

    public interface IInsurance
    {
        string GetInsurance();
    }
}