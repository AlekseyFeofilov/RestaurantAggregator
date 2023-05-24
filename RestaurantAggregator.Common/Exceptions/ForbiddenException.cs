using Microsoft.AspNetCore.Http;

namespace RestaurantAggregator.Common.Exceptions;

public class ForbiddenException : ExceptionWithStatusCode
{
    public override int StatusCode => StatusCodes.Status403Forbidden;
}