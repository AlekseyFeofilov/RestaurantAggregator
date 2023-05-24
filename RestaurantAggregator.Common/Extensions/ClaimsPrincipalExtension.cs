using System.Security.Claims;
using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Common.Extensions;

public static class ClaimsPrincipalExtension
{
    public static Guid GetNameIdentifier(this ClaimsPrincipal claimsPrincipal)
    {
        var nameIdentifier = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

        if (nameIdentifier == null)
        {
            throw new InvalidClaimPrincipalException();
        }
        
        return Guid.Parse(nameIdentifier.Value);
    }
}