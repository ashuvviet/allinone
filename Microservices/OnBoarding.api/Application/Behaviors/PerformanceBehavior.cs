using MediatR;
using System.Diagnostics;

namespace OnBoarding.api.Application.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> logger;

        public PerformanceBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Sarting Performace measurement for type {typeof(TRequest).Name}");
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                return next();
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occured");
                throw;
            }
            finally { 
                stopwatch.Stop();
                logger.LogInformation("total time taken by {RequestName} is {ElapsedMilliseconds} milliseconds", typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
