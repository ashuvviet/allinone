// Copyright SCIEX 2021. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Behaviors
{
    public class
        ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>>
            _logger;

        /// <summary>
        /// validator hold fluent validation result.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">The validators.</param>
        /// <param name="logger">The logger.</param>
        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validators =
                validators ?? throw new ArgumentNullException(nameof(validators));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            logger.LogDebug("----- INSTANCE CREATED - {@ClassName}", GetType().Name,
                GetType().Assembly.FullName);
        }

        public async Task<TResponse> Handle(TRequest request
            , CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = typeof(TRequest).Name;

            _logger.LogInformation("----- Validating command {CommandType}", typeName);

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!failures.Any())
            {
                _logger.LogDebug("Request {@Request} Validated Successfully", typeName);
                return await next();
            }

            _logger.LogWarning(
                "Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}"
                , typeName, request, failures);

            throw new ValidationException("Validation exception", failures);
        }
    }
}
