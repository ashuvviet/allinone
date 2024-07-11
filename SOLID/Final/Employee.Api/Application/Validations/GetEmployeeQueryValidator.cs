using EmployeeWebApi.Application.Query;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebApi.Application.Validations
{
    public class GetEmployeeQueryValidator : AbstractValidator<GetEmployee>
    {
        public GetEmployeeQueryValidator(ILogger<GetEmployeeQueryValidator> logger)
        {
            logger.LogDebug("----- Validation Started for - {ClassName}", GetType().Name);


            RuleFor(command => command).NotNull();

            logger.LogDebug("----- Validation Complted for - {ClassName}", GetType().Name);
        }
    }
}
