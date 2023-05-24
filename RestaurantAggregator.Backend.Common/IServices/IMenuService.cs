using RestaurantAggregator.Backend.Common.Dtos.Menu;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IMenuService
{
    Task<IEnumerable<MenuDto>> FetchMenus(Guid restaurantId, int? page);
}