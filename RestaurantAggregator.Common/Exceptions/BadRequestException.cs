using Microsoft.AspNetCore.Http;

namespace RestaurantAggregator.Common.Exceptions;

public abstract class BadRequestException : ExceptionWithStatusCode
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException()
    {
    }
}