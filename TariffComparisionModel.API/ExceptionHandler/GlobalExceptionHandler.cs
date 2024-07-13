using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TariffComparisionModel.API.ExceptionHandler
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceID = httpContext.TraceIdentifier;
            _logger.LogError(exception, "Could not process a request on machine {MachineName}. TraceID:{TraceID}", Environment.MachineName, traceID);

            var (statusCode, title) =MapExceptions(exception);
            await Results.Problem(
                title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>
                {
                    {"traceId",traceID }
                }
                ).ExecuteAsync(httpContext);

            return true;

        }
        private static (int StatusCode, string Title) MapExceptions(Exception exception)
        {
            return exception switch
            {
                ArgumentNullException=>(StatusCodes.Status400BadRequest,exception.Message),
                ArgumentException=> (StatusCodes.Status400BadRequest, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "There is an internal server error. Please contact support if this issue persists.")
            };
        }
    }
}
