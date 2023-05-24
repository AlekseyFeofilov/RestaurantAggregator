using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Auth.Common.Exceptions.BadRequestExceptions;

public class InvalidPasswordException : BadRequestException
{
    public InvalidPasswordException(string message) : base(message)
    {
    }
}