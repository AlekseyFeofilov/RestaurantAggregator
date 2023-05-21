using RestaurantAggregator.Backend.Common.Configurations;

namespace RestaurantAggregator.Backend.API.Middleware;

public class ErrorHandlingMiddleware // todo попробовать сделать через IErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this._next = next;
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
            
            var message = AppConfigurations.isDevelopmentEnvironment ? exception.Message : "";
            await WriteResponse(context, StatusCodes.Status500InternalServerError, message);
        }
    }

    private async Task WriteResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = AppConfigurations.ResponseContentType;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(message);
    }
}