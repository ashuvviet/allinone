using Customers.Api.Application.Commands;
using Customers.Api.Application.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Application.Validators
{
    public class GetCustomerIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerIdQueryValidator()
        {
            RuleFor(query => query.CustomerId).NotEmpty();
        }
    }

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command.CompanyName).NotEmpty().MinimumLength(4).WithMessage("Company Name length should be mor than 4");

            RuleFor(c => c.City).NotEmpty();

            RuleFor(c => c.Name).NotEmpty();

            RuleFor(c => c.PhoneNumber).NotEmpty();
        }
    }
}
