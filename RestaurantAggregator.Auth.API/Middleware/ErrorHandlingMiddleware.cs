using RestaurantAggregator.Auth.Common.Configuration;
using RestaurantAggregator.Auth.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Configurations;

namespace RestaurantAggregator.Auth.API.Middleware;

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
        catch (InvalidAccessOrRefreshToken)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        catch (InvalidEmailOrPasswordException)
        {
            await WriteResponse(context, StatusCodes.Status400BadRequest, "Invalid email or password");
        }
        catch (InvalidTokenException)
        {
            await WriteResponse(context, StatusCodes.Status400BadRequest, "Invalid Token");
        }
        catch (InvalidUserException exception)
        {
            await WriteResponse(context, StatusCodes.Status400BadRequest, exception.Message);
        }
        catch (Exception exception)
        {
            Console.Write(exception);
            
            var message = Configurations.isDevelopmentEnvironment ? exception.Message : "";
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