using System.Security.Claims;

namespace RestaurantAggregator.BL.Services;

public interface IUserService
{
    Guid GetUserId(ClaimsPrincipal claimsPrincipal);
}

public class UserService : IUserService
{
    public Guid GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        return Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}