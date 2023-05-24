using Microsoft.Extensions.Options;
using RestaurantAggregator.Backend.Common.Configurations;

namespace RestaurantAggregator.Backend.API.Middleware;

public class ErrorHandlingMiddleware // todo попробовать сделать через IErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    
    private readonly IOptions<AppConfigurations> _configurations;

    public ErrorHandlingMiddleware(RequestDelegate next, IOptions<AppConfigurations> configurations)
    {
        this._next = next;
        _configurations = configurations;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            Console.Write(exception);
            
            var message = _configurations.Value.IsDevelopmentEnvironment ? exception.Message : "";
            await WriteResponse(context, StatusCodes.Status500InternalServerError, message);
        }
    }

    private async Task WriteResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json"; // todo сделать через констранту (как и роуты)
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(message);
    }
}