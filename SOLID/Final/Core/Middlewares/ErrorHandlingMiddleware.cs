// Copyright SCIEX 2021. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Core.Middlewares
{
    /// <summary>
    /// Middle ware that http request
    /// So it handles the error happen on all the controller
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }


        /// <summary>
        /// Handle a exception and return error for requester
        /// </summary>
        /// <param name="context">HTTP context of request</param>
        /// <param name="exception">Exception details</param>
        /// <returns></returns>
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private int GetStatusCode(Exception exception) =>
            exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

        private string GetTitle(Exception exception) =>
            exception switch
            {
                _ => "Server Error"
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