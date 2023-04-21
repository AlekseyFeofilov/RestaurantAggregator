namespace RestaurantAggregator.Common.Models.Dto;


public class OrderOptions
{
    public bool Current { get; set; }
    
    public int? NumberStartWith { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public int? Page { get; set; }

    public OrderOptions(bool current, int? numberStartWith, DateTime? startDate, DateTime? endDate, int? page)
    {
        Current = current;
        NumberStartWith = numberStartWith;
        StartDate = startDate;
        EndDate = endDate;
        Page = page;
    }

    public OrderOptions()
    {
    }
}