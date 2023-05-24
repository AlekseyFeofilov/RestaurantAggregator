using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.AdminPanel.Models.Shared;

public class PagedNamedListModel
{
    public string Contains { get; set; }
    
    public PageInfo PageInfo { get; set; }
    
    public IEnumerable<NamedItem> NamedItems { get; set; }

    public PagedNamedListModel(string contains, PageInfo pageInfo, IEnumerable<NamedItem> namedItems)
    {
        Contains = contains;
        PageInfo = pageInfo;
        NamedItems = namedItems;
    }
}