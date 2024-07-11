using Customers.Domain.Models;
using Customers.Domain.Repositories;
using Customers.Infra.Options;
using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Customers.Infra.Repositories
{
    public class LiteDBContext : ILiteDBContext
    {
        private readonly LiteDatabase _context;
        private static ILiteCollection<Customer> collection;
        private static ILiteCollection<Support> supportCollection;
        private string nameOfCollection = "Customers";
        private string SupportCollection = "Supports";

        public LiteDBContext(IOptions<DbConfig> configs)
        {
            try
            {
                if (_context == null)
                {
                    _context = new LiteDatabase($"Filename={configs.Value.PathToDB};Connection=Direct");
                    collection = _context.GetCollection<Customer>(nameOfCollection);
                    supportCollection = _context.GetCollection<Support>(SupportCollection);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }

        public BsonValue InsertCustomer(Customer customer)
        {
            collection = _context.GetCollection<Customer>(nameOfCollection);

            var bsonValue = collection.Insert(customer);

            return bsonValue;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = collection.FindAll();

            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            var bs = new BsonValue(customerId);

            var customer = collection.FindById(bs);

            return customer;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var hasUpdated = collection.Update(customer);

            return hasUpdated;
        }

        public bool DeleteCustomerById(int customerId)
        {
            var bs = new BsonValue(customerId);

            var hasDeleted = collection.Delete(bs);

            return hasDeleted;
        }


        public Support GetSupportByCustomerId(int customerId)
        {
            var bs = new BsonValue(customerId);

            var support = supportCollection.FindById(bs);

            return support;
        }

        public BsonValue InsertSupport(Support support)
        {
            supportCollection = _context.GetCollection<Support>(nameOfCollection);

            var bsonValue = supportCollection.Insert(support);

            return bsonValue;
        }
    }
}