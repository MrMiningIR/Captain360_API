using Capitan360.Application.Common;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Capitan360.Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, IUnitOfWork unitOfWork) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Exception? exception = null;
        try
        {
            await next.Invoke(context);
        }
        catch (ValidationException validationException)
        {
            exception = validationException;
            await HandleExceptionAsync(context, validationException, logger, HttpStatusCode.BadRequest);
        }
        catch (UnExpectedException unExpectedException)
        {
            exception = unExpectedException;
            await HandleExceptionAsync(context, unExpectedException, logger, HttpStatusCode.InternalServerError);
        }
        catch (ConflictException conflictException)
        {
            exception = conflictException;
            await HandleExceptionAsync(context, conflictException, logger, HttpStatusCode.Conflict);
        }
        catch (UnAuthorizedException authorizedException)
        {
            exception = authorizedException;
            await HandleExceptionAsync(context, authorizedException, logger, HttpStatusCode.Unauthorized);
        }
        catch (ForbiddenException forbiddenException)
        {
            exception = forbiddenException;
            await HandleExceptionAsync(context, forbiddenException, logger, HttpStatusCode.Forbidden);
        }
        catch (ForbiddenForceLogoutException forbiddenForceLogoutException)
        {
            exception = forbiddenForceLogoutException;
            await HandleExceptionAsync(context, forbiddenForceLogoutException, logger, HttpStatusCode.Forbidden);
        }
        catch (UserAlreadyExistsException alreadyExistsException)
        {
            exception = alreadyExistsException;

            await HandleExceptionAsync(context, alreadyExistsException, logger, HttpStatusCode.Conflict);
        }
        catch (NotFoundException notFoundExceptions)
        {
            exception = notFoundExceptions;
            await HandleExceptionAsync(context, notFoundExceptions, logger, HttpStatusCode.NotFound);
        }
        catch (DbUpdateException dbUpdateException)
        {
            exception = dbUpdateException;

            await HandleExceptionAsync(context, dbUpdateException, logger, HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            exception = ex;
            await HandleExceptionAsync(context, ex, logger, HttpStatusCode.InternalServerError);
        }

        if (exception != null && unitOfWork.HasActiveTransaction)
        {
            await unitOfWork.RollbackTransactionAsync(context.RequestAborted);
            logger.LogInformation("Transaction rolled back due to {ExceptionType}: {Message}", exception.GetType().Name, exception.Message);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger, HttpStatusCode httpStatusCode)
    {
        switch (httpStatusCode)
        {
            case HttpStatusCode.BadRequest:
                logger.LogError("Validation error: {Message}", exception.Message);
                break;

            case HttpStatusCode.InternalServerError:
                logger.LogError(exception, "Internal server error: {Message}", exception.Message);
                break;

            case HttpStatusCode.Conflict:
                logger.LogError(exception, "Conflict: {Message}", exception.Message);
                break;

            case HttpStatusCode.Unauthorized:
                logger.LogError(exception, "Unauthorized: {Message}", exception.Message);
                break;

            case HttpStatusCode.Forbidden:
                logger.LogError(exception, "Forbidden: {Message}", exception.Message);
                break;

            case HttpStatusCode.NotFound:
                logger.LogWarning(exception, "Not found: {Message}", exception.Message);
                break;

            default:
                logger.LogInformation("Handled exception: {Message}", exception.Message);
                break;
        }

        // ساخت پاسخ JSON یکدست
        //var result = JsonSerializer.Serialize(new ApiResponse<object>((int)httpStatusCode, exception.Message, new
        //{
        //    Details = httpStatusCode == HttpStatusCode.InternalServerError ? "An unexpected error occurred." : null
        //}));

        var response = new ApiResponse<string>((int)httpStatusCode, exception.Message,
            (httpStatusCode == HttpStatusCode.InternalServerError ? "An unexpected error occurred." : null),
            exception is ForbiddenForceLogoutException);

        var options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // جلوگیری از اسکیپ یونیکد
        };

        var result = JsonSerializer.Serialize(response, options);

        //context.Response.ContentType = "application/json";
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = (int)httpStatusCode;
        return context.Response.WriteAsync(result);
    }
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