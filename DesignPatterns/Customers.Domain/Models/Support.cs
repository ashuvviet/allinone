using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Domain.Models
{
    public class Support
    {
        public int CustomerId { get; set; }

        public int SupportId { get; set; }

        public string Query { get; set; }
    }
}
