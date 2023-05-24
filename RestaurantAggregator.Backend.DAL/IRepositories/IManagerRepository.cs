using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Backend.DAL.IRepositories;

public interface IManagerRepository : ICrudRepository<Manager>
{
}