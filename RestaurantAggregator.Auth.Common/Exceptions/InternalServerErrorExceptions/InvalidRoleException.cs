using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Auth.Common.Exceptions.InternalServerErrorExceptions;

public class InvalidRoleException : InternalServerErrorException
{
    public InvalidRoleException(string message) : base(message)
    {
    }
}