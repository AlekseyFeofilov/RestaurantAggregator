using Microsoft.AspNetCore.Authorization;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationFailureReasons;

public class InvalidClaimPrincipalReason : AuthorizationFailureReason
{
    public InvalidClaimPrincipalReason(IAuthorizationHandler handler, string message) : base(handler, message)
    {
    }
}