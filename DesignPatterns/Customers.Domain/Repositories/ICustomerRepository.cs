using Customers.Domain.Models;
using LiteDB;
using System;
using System.Collections.Generic;

namespace Customers.Domain.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        BsonValue InsertCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomerById(int customerId);
    }

}
