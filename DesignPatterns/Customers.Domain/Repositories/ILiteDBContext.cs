using Customers.Domain.Models;
using LiteDB;
using System.Collections.Generic;

namespace Customers.Domain.Repositories
{
    public interface ILiteDBContext
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        BsonValue InsertCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomerById(int customerId);

        BsonValue InsertSupport(Support support);

        Support GetSupportByCustomerId(int customerId);
    }
}