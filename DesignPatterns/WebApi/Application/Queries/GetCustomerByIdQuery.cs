using Customers.Api.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerResponse>
    {
        public int CustomerId { get; set; }
    }
}
