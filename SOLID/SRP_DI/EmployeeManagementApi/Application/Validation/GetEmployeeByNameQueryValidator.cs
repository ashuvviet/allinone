using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using EmployeeManagementApi.Application.Commands;
using EmployeeManagementApi.Application.Query;
using FluentValidation;

namespace EmployeeManagementApi.Application.Validation
{
    public class GetEmployeeByNameQueryValidator : AbstractValidator<GetEmployeeByNameQuery>
    {
        public GetEmployeeByNameQueryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Name is Empty");
        }
    }
}
