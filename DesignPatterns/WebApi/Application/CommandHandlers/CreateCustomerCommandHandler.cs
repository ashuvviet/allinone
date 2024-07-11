using Customers.Api.Application.Commands;
using Customers.Api.Application.Responses;
using Customers.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Api.Application.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = _customerRepository.InsertCustomer(new Domain.Models.Customer() { Id = request.Id, Name = request.Name, City = request.City, CompanyName = request.CompanyName, PhoneNumber = request.PhoneNumber });
            return await Task.FromResult(new CreateCustomerResponse { Id = result.AsInt32 });
        }
    }
}
