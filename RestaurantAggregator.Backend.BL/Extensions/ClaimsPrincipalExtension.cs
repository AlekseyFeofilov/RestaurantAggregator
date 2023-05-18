using System.Security.Claims;

namespace RestaurantAggregator.BL.Extensions;

public static class ClaimsPrincipalExtension
{
    public static Guid GetNameIdentifier(this ClaimsPrincipal claimsPrincipal)
    {
        return Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}