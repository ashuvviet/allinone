using Customers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Application.Responses
{
    public class GetCustomersResponse
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}
