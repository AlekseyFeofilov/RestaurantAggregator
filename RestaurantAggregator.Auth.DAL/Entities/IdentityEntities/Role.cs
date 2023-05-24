using Microsoft.AspNetCore.Identity;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

public class Role: IdentityRole<Guid>
{
    public RoleType Type { get; set; }
}