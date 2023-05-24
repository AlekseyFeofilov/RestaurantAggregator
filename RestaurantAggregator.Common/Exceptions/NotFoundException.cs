using Microsoft.AspNetCore.Http;

namespace RestaurantAggregator.Common.Exceptions;

public abstract class NotFoundException : ExceptionWithStatusCode
{
    public Guid Id { get; set; }
    
    public override int StatusCode => StatusCodes.Status404NotFound;

    public NotFoundException(Guid id)
    {
        Id = id;
    }
}