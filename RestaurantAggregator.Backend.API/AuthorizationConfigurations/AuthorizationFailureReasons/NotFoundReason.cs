using Microsoft.AspNetCore.Authorization;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationFailureReasons;

public class NotFoundReason : AuthorizationFailureReason
{
    public NotFoundReason(IAuthorizationHandler handler, string message) : base(handler, message)
    {
    }
}