using System.Net;
using System.Text.Json;
using Capitan360.Domain.Exceptions;

namespace Capitan360.Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFoundExceptions)
        {
            await HandleExceptionAsync(context, notFoundExceptions, logger, HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger, HttpStatusCode.InternalServerError);
        }

    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger, HttpStatusCode httpStatusCode)
    {
        if (httpStatusCode == HttpStatusCode.NotFound)
            logger.LogWarning("{exception}", exception.Message);
        if (httpStatusCode == HttpStatusCode.InternalServerError)
            logger.LogError("{exception}", exception.Message);


        var result = JsonSerializer.Serialize(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;
        return context.Response.WriteAsync(result);
    }

}

