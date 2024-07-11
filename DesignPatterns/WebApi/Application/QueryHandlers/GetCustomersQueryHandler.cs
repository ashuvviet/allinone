using Customers.Api.Application.Queries;
using Customers.Api.Application.Responses;
using Customers.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Api.Application.QueryHandlers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomersResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = _customerRepository.GetAllCustomers();
            return await Task.FromResult(new GetCustomersResponse() { Customers = customers });
        }
    }
}
