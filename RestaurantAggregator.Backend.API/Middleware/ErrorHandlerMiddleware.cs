using Microsoft.Extensions.Options;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Exceptions.BadRequestExceptions;
using RestaurantAggregator.Backend.Common.Exceptions.ForbiddenException;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Common.Exceptions;
using CourierNotFoundException = RestaurantAggregator.Backend.Common.Exceptions.NotFoundException.CourierNotFoundException;
using ManagerNotFoundException = RestaurantAggregator.Backend.Common.Exceptions.NotFoundException.ManagerNotFoundException;

namespace RestaurantAggregator.Backend.API.Middleware;

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
                CartIsEmptyException => "Cart is empty",
                CourierNotFoundException courierNotFoundException => $"Courier with id {courierNotFoundException.Id} wasn't found",
                DishNotFoundException dishNotFoundException => $"Dish with id {dishNotFoundException.Id} wasn't found",
                ManagerNotFoundException managerNotFoundException => $"Manager with id {managerNotFoundException.Id} wasn't found",
                CookNotfoundException cookNotfoundException => $"Cook with id {cookNotfoundException.Id} wasn't found",
                MenuNotFoundException menuNotFoundException => $"Menu with id {menuNotFoundException.Id} wasn't found",
                OrderNotFoundException orderNotFoundException => $"Order with id {orderNotFoundException.Id} wasn't found",
                RestaurantNotFoundException restaurantNotFoundException => $"Restaurant with id {restaurantNotFoundException.Id} wasn't found",
                DishFromDifferentRestaurantsException dishFromDifferentRestaurantsException => $"Dish with id {dishFromDifferentRestaurantsException.FirstDishId} and dish with id {dishFromDifferentRestaurantsException.SecondDishId} are in different restaurants",
                DishInCartNotAvailableException dishInCartNotAvailableException => $"Dish with id {dishInCartNotAvailableException.Message} isn't available anymore",
                
                ForbiddenException => "",
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