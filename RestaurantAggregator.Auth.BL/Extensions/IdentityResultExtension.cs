using Microsoft.AspNetCore.Identity;

namespace RestaurantAggregator.Auth.BL.Extensions;

public static class IdentityResultExtension
{
    public static string ErrorsToString(this IdentityResult identityResult)
    {
        return string.Join("; ", identityResult.Errors.Select(x => x.Description).ToList());
    }
}