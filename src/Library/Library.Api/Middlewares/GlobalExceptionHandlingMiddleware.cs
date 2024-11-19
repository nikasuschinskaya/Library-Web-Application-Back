using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Library.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message,
                HttpStatusCode.BadRequest, "ValidationContext error!");
        }
        catch (ArgumentNullException ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message,
                HttpStatusCode.BadRequest, "Argument cant be null!");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message,
                HttpStatusCode.InternalServerError, "Something went wrong!");
        }
    }


    private async Task HandleExceptionAsync(
       HttpContext httpContext,
       string exceptionMessage,
       HttpStatusCode statusCode,
       string message)
    {
        var response = httpContext.Response;

        if (!response.HasStarted)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            var error = new
            {
                Message = message,
                ExceptionMessage = exceptionMessage,
                StatusCode = statusCode
            };

            string result = JsonSerializer.Serialize(error);

            await response.WriteAsync(result);
        }
    }
}