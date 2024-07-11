using MediatR;

namespace OnBoarding.api.Application.Behaviors
{
    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> logger;

        public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Sarting Exeception handling");
            try
            {
                return next();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occured");
                throw;
            }
        }
    }
}
