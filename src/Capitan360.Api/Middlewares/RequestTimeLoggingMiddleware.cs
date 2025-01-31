using System.Diagnostics;

namespace Capitan360.Api.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await next(context);

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            LogRequestTime(elapsedMilliseconds, context);
        }

        private void LogRequestTime(long elapsedMilliseconds, HttpContext context)
        {
            if (elapsedMilliseconds / 1000 > 3000)
            {
                logger.LogInformation("Request took {elapsedMilliseconds} s in {Method} : {Path} ",
                    elapsedMilliseconds, context.Request.Method, context.Request.Path);
            }
            if (elapsedMilliseconds / 1000 > 5000)
            {
                logger.LogWarning("Request took {elapsedMilliseconds} s", elapsedMilliseconds);
            }
        }
    }
}
