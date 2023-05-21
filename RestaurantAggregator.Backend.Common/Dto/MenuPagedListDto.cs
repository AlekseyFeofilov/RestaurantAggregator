using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.Common.Dto;

public class MenuPagedListDto
{
    public IEnumerable<MenuDto> Menus { get; }
    
    public PageInfoModel Pagination { get; }
}