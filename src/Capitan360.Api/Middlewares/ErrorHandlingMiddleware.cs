using System.Net;
using System.Text.Json;
using Capitan360.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }

        catch (UnExpectedException unExpectedException)
        {
            await HandleExceptionAsync(context, unExpectedException, logger, HttpStatusCode.InternalServerError);

        }
        catch (ConflictException conflictException)
        {
            await HandleExceptionAsync(context, conflictException, logger, HttpStatusCode.Conflict);
        }
        catch (UnAuthorizedException authorizedException)
        {
            await HandleExceptionAsync(context, authorizedException, logger, HttpStatusCode.Unauthorized);
        }
        catch (UserAlreadyExistsException alreadyExistsException)
        {

            await HandleExceptionAsync(context, alreadyExistsException, logger, HttpStatusCode.Conflict);
        }
        catch (NotFoundException notFoundExceptions)
        {
            await HandleExceptionAsync(context, notFoundExceptions, logger, HttpStatusCode.NotFound);
        }
        catch (DbUpdateException dbUpdateException)
        {

            await HandleExceptionAsync(context, dbUpdateException, logger, HttpStatusCode.InternalServerError);

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

    //TODO: Handle Unique Constraint Exception
    //    if (ex.InnerException?.Message.Contains("IX_Users_PhoneNumber") == true)
    //    {
    //        Console.WriteLine("Phone number must be unique.");
    //    }
    //else
    //{
    //    throw;
    //}
    // CK_User_PhoneNumber_Length

}

