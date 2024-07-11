using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Middlewares
{
    public class StopwatcherMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<StopwatcherMiddleware> _logger;

        public StopwatcherMiddleware(RequestDelegate next, ILogger<StopwatcherMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            await _next(context);

            var totalTime = watch.ElapsedMilliseconds;
            _logger.LogInformation($"total time taken by api {context.Request.Path} {totalTime} ms");               
        }
    }
}
