using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MySqlConnector;
using OnBoarding.api.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Registry;

namespace OnBoarding.api.HeathChecks
{
    public class MySQSHealthCheck : IHealthCheck
    {
        private readonly IOptions<DbOptions> _options;
        private readonly IReadOnlyPolicyRegistry<string> _policyRegistry;
        private readonly ILogger<MySQLHealthCheck> _logger;

        public MySQSHealthCheck(IOptions<DbOptions> options, IReadOnlyPolicyRegistry<string> policyRegistry, ILogger<MySQLHealthCheck> logger) 
        {
            this._options = options;
            this._policyRegistry = policyRegistry;
            this._logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var mysqlCircuitBreakerPolicy =
                    _policyRegistry.Get<AsyncCircuitBreakerPolicy>(
                        "MysqlCircuitBreakerPolicy");

                switch (mysqlCircuitBreakerPolicy.CircuitState)
                {
                    case CircuitState.Closed:
                    case CircuitState.HalfOpen:
                        var iSConnected = false;
                        var policyOutcome =
                            await mysqlCircuitBreakerPolicy.ExecuteAndCaptureAsync(
                                async () =>
                                {
                                    using var connection =
                                        new MySqlConnection(_options.Value.DefaultConnection);
                                    await connection.OpenAsync(cancellationToken);

                                    if (await connection.PingAsync(cancellationToken))
                                    {
                                        iSConnected = true;
                                    }
                                });

                        //check the outcome of the polly
                        if (policyOutcome.Outcome != OutcomeType.Successful ||
                            !iSConnected)
                        {
                            _logger.LogError(policyOutcome.FinalException.Message);
                            return new HealthCheckResult(
                                context.Registration.FailureStatus
                             , "Db Not Found or a failure is raised on health check for Mysql .");
                        }

                        _logger.LogInformation("mysql is Healthy");
                        return HealthCheckResult.Healthy();

                    case CircuitState.Open:
                    case CircuitState.Isolated:
                        _logger.LogInformation(
                            "Circuit is now in Open State, can not make calls");
                        return new HealthCheckResult(context.Registration.FailureStatus
                         , $"CircuitState.{mysqlCircuitBreakerPolicy.CircuitState}");
                    default:
                        return new HealthCheckResult(context.Registration.FailureStatus
                         , $"Unknown CircuitState: CircuitState.{mysqlCircuitBreakerPolicy.CircuitState}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
