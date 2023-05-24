using Microsoft.AspNetCore.SignalR;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Notification.UserProviders;

public class IdUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User.GetNameIdentifier().ToString();
    }
}
