using Core.Contracts;
using Core.Models;
using EmployeeWebApi.Application.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeWebApi.Application.QueryHandlers
{
    internal class GetEmployeeQueryHandler : IRequestHandler<GetEmployee, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeQueryHandler(ILogger<GetEmployeeQueryHandler> logger,
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployee request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAll();
        }
    }
}
