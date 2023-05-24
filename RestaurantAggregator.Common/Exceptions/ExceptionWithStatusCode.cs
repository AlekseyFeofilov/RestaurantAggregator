namespace RestaurantAggregator.Common.Exceptions;

public abstract class ExceptionWithStatusCode : Exception
{
    public abstract int StatusCode { get; }
    
    public ExceptionWithStatusCode(string message) : base(message)
    {
    }

    public ExceptionWithStatusCode()
    {
    }
}