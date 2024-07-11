using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebApi.Application.Query
{
    public class GetEmployee : IRequest<IEnumerable<Employee>>
    {
    }
}
