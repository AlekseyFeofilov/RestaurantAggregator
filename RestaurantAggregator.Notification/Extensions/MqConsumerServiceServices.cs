namespace RestaurantAggregator.Notification.Services.MqServices;

public static class MqConsumerServiceServices
{
    public static void AddMqConsumerService(this IServiceCollection services)
    {
        services.AddSingleton<IStatusChangeListenerService, StatusChangeListenerService>();
    }
    
    public static Task StartMqConsumerService(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var service = serviceScope.ServiceProvider.GetService<IStatusChangeListenerService>();
        service?.Start();

        return Task.CompletedTask;
    }
}