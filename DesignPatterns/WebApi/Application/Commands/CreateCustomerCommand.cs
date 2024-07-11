using Customers.Api.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Application.Commands
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }

        public string CompanyName { get; set; }
    }
}
