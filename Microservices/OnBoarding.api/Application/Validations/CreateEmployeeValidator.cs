using FluentValidation;
using OnBoarding.api.Application.Commands;
using OnBoarding.api.Repositories;

namespace OnBoarding.api.Application.Validations
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(command => command.Employee).NotNull();

            RuleFor(command => command.Employee.FirstName).NotNull().NotEmpty().WithMessage("Employee First name is Empty")
                .MaximumLength(256).WithMessage("first name length is more than 256 char");


            RuleFor(command => command.Employee.LastName).NotNull().NotEmpty().WithMessage("Employee last name is Empty")
                .MaximumLength(256).WithMessage("last name length is more than 256 char");


            RuleFor(command => command.Employee.Email).NotNull().NotEmpty().WithMessage("Employee email is Empty");

            //RuleFor(command => command.Employee.Email).MustAsync(async (s, c) =>
            //{
            //    return await employeeRepository.Get(s) == null;
            //}).WithMessage("email already exist");
        }
    }
}
