using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IStatusChangingMessageService
{
    public void SendStatusChangingMessage(StatusChangingNotification notification);
}