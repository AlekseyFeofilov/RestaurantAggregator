using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.Common.Dtos.Menu;

public class MenuPagedListDto
{
    public IEnumerable<MenuDto> Menus { get; }
    
    public PageInfo Pagination { get; }
}