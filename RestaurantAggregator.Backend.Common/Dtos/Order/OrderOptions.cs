namespace RestaurantAggregator.Backend.Common.Dtos.Order;


public class OrderOptions
{
    public bool Current { get; set; }
    
    public string? NumberContains { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public int Page { get; set; }

    public OrderOptions(bool current, string? numberContains, DateTime? startDate, DateTime? endDate, int page)
    {
        Current = current;
        NumberContains = numberContains;
        StartDate = startDate;
        EndDate = endDate;
        Page = page;
    }

    public OrderOptions()
    {
    }
}