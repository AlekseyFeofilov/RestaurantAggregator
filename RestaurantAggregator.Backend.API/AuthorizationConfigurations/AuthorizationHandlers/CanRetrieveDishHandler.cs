using RestaurantAggregator.Backend.API.AuthorizationConfigurations.Requirements;
using RestaurantAggregator.Backend.DAL.Repositories.DishRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationHandlers;

public class CanRetrieveDishHandler : HasAccessToDishHandler<CanRetrieveDishRequirement>
{
    public CanRetrieveDishHandler(IHttpContextAccessor contextAccessor, IManagerRepository managerRepository, IDishRepository dishRepository) : base(contextAccessor, managerRepository, dishRepository)
    {
    }

    protected override Task<Guid> GetDishId()
    {
        return Task.FromResult(Guid.Parse((string)ContextAccessor.HttpContext!.GetRouteData().Values["dishId"]!)); //crutch 
    }
}