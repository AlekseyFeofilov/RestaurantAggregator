using RestaurantAggregator.Backend.API.Extensions;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.API.Requirements;
using RestaurantAggregator.Backend.DAL.Repositories.DishRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;

namespace RestaurantAggregator.Backend.API.AuthorizationHandlers;

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