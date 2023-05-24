using RestaurantAggregator.Backend.API.AuthorizationConfigurations.Requirements;
using RestaurantAggregator.Backend.DAL.IRepositories;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationHandlers;

public class CanDeleteDishHandler : HasAccessToDishHandler<CanDeleteDishRequirement>
{
    public CanDeleteDishHandler(IHttpContextAccessor contextAccessor, IManagerRepository managerRepository, IDishRepository dishRepository) : base(contextAccessor, managerRepository, dishRepository)
    {
    }

    protected override Task<Guid> GetDishId()
    {
        return Task.FromResult(Guid.Parse((string)ContextAccessor.HttpContext!.GetRouteData().Values["dishId"]!)); //crutch 
    }
}