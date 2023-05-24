using Microsoft.AspNetCore.Http;

namespace RestaurantAggregator.Common.Exceptions;

public class InternalServerErrorException : ExceptionWithStatusCode
{
    public override int StatusCode => StatusCodes.Status500InternalServerError;
    
    public InternalServerErrorException(string message) : base(message)
    {
    }

    public InternalServerErrorException()
    {
    }
}