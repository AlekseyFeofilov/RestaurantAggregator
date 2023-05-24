using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Common.Dtos;

public class StatusChangingNotification
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public string OrderNumber { get; set; }
    
    [Required]
    public OrderStatus OrderStatus { get; set; }

    public StatusChangingNotification(Guid userId, string orderNumber, OrderStatus orderStatus)
    {
        UserId = userId;
        OrderNumber = orderNumber;
        OrderStatus = orderStatus;
    }
}