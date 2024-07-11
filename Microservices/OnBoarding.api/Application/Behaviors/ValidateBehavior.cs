using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace OnBoarding.api.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var failures = new List<ValidationFailure>();
            foreach (var validationFailure in _validators)
            {
                var errors = await validationFailure.ValidateAsync(request, cancellationToken);
                if (errors != null && errors.Errors != null)
                {
                    failures.AddRange(errors.Errors);
                }
            }

            if(!failures.Any())
            {
                return await next();
            }

            throw new FluentValidation.ValidationException("Validation Exception", failures);
        }
    }
}
