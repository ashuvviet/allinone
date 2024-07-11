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
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAll();
        }
    }
}
