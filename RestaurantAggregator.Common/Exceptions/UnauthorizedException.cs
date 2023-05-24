using Microsoft.AspNetCore.Http;

namespace RestaurantAggregator.Common.Exceptions;

public abstract class UnauthorizedException : ExceptionWithStatusCode
{
    public override int StatusCode => StatusCodes.Status401Unauthorized;
    
    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException()
    {
    }
}