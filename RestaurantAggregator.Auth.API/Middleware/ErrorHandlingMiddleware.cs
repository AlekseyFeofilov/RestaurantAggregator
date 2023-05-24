using Microsoft.Extensions.Options;
using RestaurantAggregator.Auth.Common.Exceptions;
using RestaurantAggregator.Auth.Common.Exceptions.BadRequestExceptions;
using RestaurantAggregator.Auth.Common.Exceptions.InternalServerErrorExceptions;
using RestaurantAggregator.Auth.Common.Exceptions.NotFoundExceptions;
using RestaurantAggregator.Auth.Common.Exceptions.UnauthorizedExceptions;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Common.Exceptions;
using AppConfigurations = RestaurantAggregator.Auth.Common.Configurations.AppConfigurations;

namespace RestaurantAggregator.Auth.API.Middleware;

public class ErrorHandlingMiddleware
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
        var message = "";
        ExceptionWithStatusCode exception;

        try
        {
            await _next(context);
            return;
        }
        catch (ExceptionWithStatusCode e)
        {
            exception = e;

            message = e switch
            {
                InvalidPasswordException invalidPasswordException => invalidPasswordException.Message,
                InvalidEmailOrPasswordException => "Invalid email or password",
                CookNotFoundException cookNotfoundException => $"Cook with id {cookNotfoundException.Id} wasn't found",
                CourierNotFoundException courierNotFoundException => $"Courier with id {courierNotFoundException.Id} wasn't found",
                CustomerNotFoundException customerNotFoundException => $"Customer with id {customerNotFoundException.Id} wasn't found",
                ManagerNotFoundException managerNotFoundException => $"Manager with id {managerNotFoundException.Id} wasn't found",
                UserNotFoundException userNotFoundException => $"User with id {userNotFoundException.Id} wasn't found",
                InvalidTokenException => "Invalid token",
                InvalidAccessOrRefreshToken => "Invalid access or refresh token",
                InvalidUserException invalidUserException => invalidUserException.Message,
                InvalidRoleException invalidRoleException => invalidRoleException.Message,
                
                InternalServerErrorException => "",
                NotFoundException => "",
                UnauthorizedException => "",
                BadRequestException => "",
                _ => ""
            };
        }
        catch (Exception e)
        {
            exception = new InternalServerErrorException(e.Message);
        }

        await WriteResponse(context, exception, message);
    }

    private async Task WriteResponse(HttpContext context, ExceptionWithStatusCode exception, string message)
    {
        context.Response.StatusCode = exception.StatusCode;

        if (exception.StatusCode == StatusCodes.Status500InternalServerError)
        {
            Console.Write(exception);
            message = _configurations.Value.IsDevelopmentEnvironment ? exception.Message : "";
        }
        
        if (message == "") return;
        
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(message);
        
    }
}