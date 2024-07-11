using Core;
using Core.Models;
using EmployeeManagementApi.Application.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Application.QueryHandlers
{
    public class GetEmployeeByNameQueryHandler : IRequestHandler<GetEmployeeByNameQuery, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByNameQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(GetEmployeeByNameQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetByName(request.Name);
        }
    }
}
