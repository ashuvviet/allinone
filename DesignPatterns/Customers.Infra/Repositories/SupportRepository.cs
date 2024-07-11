using Customers.Domain.Models;
using Customers.Domain.Repositories;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infra.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly ILiteDBContext _context;

        public SupportRepository(ILiteDBContext context)
        {
            _context = context;
        }

        public Support GetSupportByCustomerId(int customerId)
        {
            return _context.GetSupportByCustomerId(customerId);
        }

        public BsonValue InsertSupport(Support support)
        {
            return _context.InsertSupport(support);
        }
    }
}
