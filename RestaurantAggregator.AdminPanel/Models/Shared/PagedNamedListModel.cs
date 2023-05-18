using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.AdminPanel.Models;

public class PagedNamedListModel
{
    public string Contains { get; set; }
    
    public PageInfoModel PageInfoModel { get; set; }
    
    public IEnumerable<NamedItem> NamedItems { get; set; }

    public PagedNamedListModel(string contains, PageInfoModel pageInfoModel, IEnumerable<NamedItem> namedItems)
    {
        Contains = contains;
        PageInfoModel = pageInfoModel;
        NamedItems = namedItems;
    }
}