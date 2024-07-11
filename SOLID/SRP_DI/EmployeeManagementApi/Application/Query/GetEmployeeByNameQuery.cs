using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Application.Query
{
    public class GetEmployeeByNameQuery : IRequest<Employee>
    {
        public string Name { get; set; }
    }
}
