using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Auth.Common.Exceptions.InternalServerErrorExceptions;

public class InvalidUserException : InternalServerErrorException
{
    public InvalidUserException(string message) : base(message)
    {
    }
}