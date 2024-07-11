using FluentValidation;
using moviebooking_api.Commands;
using System.Resources;

namespace moviebooking_api.Application.Validations
{
    public class CreateCinemaCommandValidator : AbstractValidator<CreateCinemaCommand>
    {
        public CreateCinemaCommandValidator()
        {
            RuleFor(command => command.Cienema.Name)
                .NotEmpty().WithMessage("Cinema Name is empty/null");

            RuleFor(command => command.Cienema.City)
                .NotEmpty().WithMessage("City Name is empty/null");
        }
    }
}
