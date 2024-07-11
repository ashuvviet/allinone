using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Application.Query
{
    public class GetAllEmployeeQuery : IRequest<IEnumerable<Employee>>
    {
    }
}
