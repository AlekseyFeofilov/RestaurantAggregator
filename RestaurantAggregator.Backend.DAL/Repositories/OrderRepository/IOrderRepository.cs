using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.BL.IRepositories;

public interface IOrderRepository
{
    Task<Order> FetchOrder(Guid orderId);
}