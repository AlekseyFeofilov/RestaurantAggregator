using Microsoft.AspNetCore.Identity;
using RestaurantAggregator.Auth.Common.Models.Enums;

namespace RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

public class Role: IdentityRole<Guid>
{
    public RoleType Type { get; set; }
}