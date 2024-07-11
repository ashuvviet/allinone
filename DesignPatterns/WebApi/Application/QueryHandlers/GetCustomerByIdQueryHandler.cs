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
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.GetCustomerById(request.CustomerId);
            return await Task.FromResult(new GetCustomerResponse() { Customer = customer });
        }
    }
}
