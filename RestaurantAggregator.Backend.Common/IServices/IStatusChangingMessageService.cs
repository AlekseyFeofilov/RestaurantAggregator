using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IStatusChangingMessageService
{
    public void SendStatusChangingMessage(StatusChangingNotification notification);
}