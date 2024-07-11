using FluentValidation;
using Newtonsoft.Json;
using OnBoarding.api.Exceptions;
using System.Diagnostics;

namespace OnBoarding.api.Middelware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger _logger;

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = GetStatusCode(exception);

            var response = new
            {
                status = code,
                details = exception.Message,
                validationErrors = GetErrors(exception),
                stacktrace = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? exception.StackTrace : null
            };

            var result = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }

        private int GetStatusCode(Exception ex) =>
            ex switch
            {
                ApiException => (ex as ApiException).StatusCode,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

        private IEnumerable<string> GetErrors(Exception exception)
        {
            IEnumerable<string> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.Select(x => x.ErrorMessage);
            }

            return errors;
        }
    }
}
