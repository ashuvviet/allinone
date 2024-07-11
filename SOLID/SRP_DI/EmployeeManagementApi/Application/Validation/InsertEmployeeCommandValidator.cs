using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using EmployeeManagementApi.Application.Commands;
using FluentValidation;

namespace EmployeeManagementApi.Application.Validation
{
    public class InsertEmployeeCommandValidator : AbstractValidator<InsertEmployeCommand>
    {
        public InsertEmployeeCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().NotNull().WithMessage("First Name is Empty");
            RuleFor(c => c.LastName).NotEmpty().NotNull().WithMessage("Last Name is Empty");
            RuleFor(c => c.Email).NotEmpty().NotNull().WithMessage("Email is Empty");
        }
    }
}
