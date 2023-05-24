namespace RestaurantAggregator.Backend.Common.Configurations;

public class AppConfigurations //todo инкапсулировать на несколько конфигураций, чтобы PageSize было в другом классе
{
    public int PageSize { get; set; }
    
    public bool IsDevelopmentEnvironment { get; set; }
}