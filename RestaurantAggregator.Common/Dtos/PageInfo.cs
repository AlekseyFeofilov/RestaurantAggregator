using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Dtos;

public class PageInfo
{
    [JsonPropertyName("size")]
    public int Size { get; }
    
    [JsonPropertyName("count")]
    public int Count { get; }
    
    [JsonPropertyName("current")]
    public int Current { get; }

    public PageInfo(int size, int count, int current)
    {
        Size = size;
        Count = count;
        Current = current;
    }
}