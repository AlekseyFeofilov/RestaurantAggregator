using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.BL.Services;

public class StatusChangingMessageService : IStatusChangingMessageService
{
    public void SendStatusChangingMessage(StatusChangingNotification notification)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "StatusChanging",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = JsonSerializer.Serialize(notification);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: "StatusChanging",
            basicProperties: null,
            body: body);
    }
}