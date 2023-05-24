using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace RestaurantAggregator.Notification.Hubs;

[Authorize]
public class NotificationsHub : Hub
{
}

