using RestaurantAggregator.BL.IRepositories;
using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<Order> FetchOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }
}