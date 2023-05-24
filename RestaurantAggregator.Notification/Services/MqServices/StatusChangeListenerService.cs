using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestaurantAggregator.Common.Dtos;
using RestaurantAggregator.Notification.Hubs;

namespace RestaurantAggregator.Notification.Services.MqServices;

public class StatusChangeListenerService : IStatusChangeListenerService
{
    private readonly IHubContext<NotificationsHub> _hubContext;

    public StatusChangeListenerService(IHubContext<NotificationsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task Start()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "StatusChanging",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var notification = JsonSerializer.Deserialize<StatusChangingNotification>(message);

            if (notification != null) 
            {
                _hubContext.Clients.User(notification.UserId.ToString()).SendAsync("ReceiveMessage",
                    $"{DateTime.UtcNow.ToString("s")} UTC: " +
                    $"order with number {notification.OrderNumber} has {notification.OrderStatus} status");
            }
            else //todo посмотреть, как обарбатываются ошибки с RabbitMQ
            {
                Console.WriteLine($"Notification is broken or smt. Message was received: {message}");
            }
        };
        
        channel.BasicConsume(queue: "StatusChanging",
            autoAck: true,
            consumer: consumer);
        
        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();

        return Task.CompletedTask;
    }
}