using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Auth.Common.Exceptions.UnauthorizedExceptions;

public class InvalidTokenException : UnauthorizedException
{
    public InvalidTokenException(string message) : base(message)
    {
    }

    public InvalidTokenException()
    {
    }
}