using Customers.Domain.Models;
using Customers.Domain.Repositories;
using LiteDB;
using System;
using System.Collections.Generic;

namespace Customers.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILiteDBContext _context;

        public CustomerRepository(ILiteDBContext context)
        {
            _context = context;
        }

        public BsonValue InsertCustomer(Customer customer)
        {
            var bsonValue = _context.InsertCustomer(customer);

            return bsonValue;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = _context.GetAllCustomers();

            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            var customer = _context.GetCustomerById(customerId);

            return customer;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var hasUpdated = _context.UpdateCustomer(customer);

            return hasUpdated;
        }

        public bool DeleteCustomerById(int customerId)
        {
            var hasDeleted = _context.DeleteCustomerById(customerId);

            return hasDeleted;
        }
    }
}
