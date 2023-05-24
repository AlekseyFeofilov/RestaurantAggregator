using RestaurantAggregator.Backend.API.AuthorizationConfigurations.Requirements;
using RestaurantAggregator.Backend.API.Extensions;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.DAL.IRepositories;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationHandlers;

public class CanModifyDishHandler : HasAccessToDishHandler<CanModifyDishRequirement>
{
    public CanModifyDishHandler(IHttpContextAccessor contextAccessor, IManagerRepository managerRepository, IDishRepository dishRepository) : base(contextAccessor, managerRepository, dishRepository)
    {
    }

    protected override async Task<Guid> GetDishId()
    {
        return (await ContextAccessor.ReadRequestBodyAsync<DishModifyModel>()).Id;
    }
}