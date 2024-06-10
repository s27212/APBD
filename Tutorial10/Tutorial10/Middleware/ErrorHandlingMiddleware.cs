using System.Net;
using Tutorial10.Exceptions;

namespace Tutorial10.Properties.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is InvalidRequestDataException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        else
        {
            _logger.LogError("Unexpected error has occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        
        context.Response.ContentType = "application/json";
        
        var response = new
        {
            error = new
            {
                message = "An error occurred while processing request.",
                detail = exception.Message
            }
        };
        
        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }
}