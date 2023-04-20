namespace RestaurantAggregator.Auth.Common.Exceptions;

public class InvalidTokenException : Exception
{
    public InvalidTokenException(Exception? innerException) : base("", innerException)
    {
    }

    public InvalidTokenException()
    {
    }
}