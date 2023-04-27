namespace RestaurantAggregator.Common.Models.Dto;

public class MenuPagedListDto
{
    public IEnumerable<MenuDto> Menus { get; }
    
    public PageInfoModel Pagination { get; }
}