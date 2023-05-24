using Microsoft.Extensions.Options;
using RestaurantAggregator.Auth.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Configurations;
using AppConfigurations = RestaurantAggregator.Auth.Common.Configurations.AppConfigurations;

namespace RestaurantAggregator.Auth.API.Middleware;

public class ErrorHandlingMiddleware // todo попробовать сделать через IErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IOptions<AppConfigurations> _configurations;

    public ErrorHandlingMiddleware(RequestDelegate next, IOptions<AppConfigurations> configurations)
    {
        _next = next;
        _configurations = configurations;
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
            
            var message = _configurations.Value.IsDevelopmentEnvironment ? exception.Message : "";
            await WriteResponse(context, StatusCodes.Status500InternalServerError, message);
        }
    }

    private async Task WriteResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(message);
    }
}