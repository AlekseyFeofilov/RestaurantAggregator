using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;
using RestaurantAggregator.Backend.Common.IServices;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class RestaurantController : Controller
{
    private readonly IRestaurantService _restaurantService;

    private readonly IMapper _mapper;

    public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
    {
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    public async Task<ActionResult> Index(string contains = "", int page = 1)
    {
        var restaurants = await _restaurantService.FetchRestaurantsAsync(contains, page);
        var pagedNamedListModel = new PagedNamedListModel(
            contains,
            restaurants.Pagination,
            restaurants.Items.Select(x => new NamedItem(x.Id, x.Name))
        );
        
        return View("Index", pagedNamedListModel);
    }

    public async Task<ActionResult> Search(string contains = "") //todo make normalized name in database
    {
        return await Index(contains, 1);
    }
    
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateRestaurantModel createRestaurantModel)
    {
        await _restaurantService.CreateRestaurantAsync(_mapper.Map<CreateRestaurantDto>(createRestaurantModel));
        return await Index(createRestaurantModel.Name);
    }
    
    public async Task<ActionResult> Edit(Guid id)
    {
        var restaurant = await _restaurantService.FetchRestaurantDetailsAsync(id);
        return View(_mapper.Map<RestaurantModel>(restaurant));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(ModifyRestaurantModel modifyRestaurantModel)
    {
        await _restaurantService.ModifyRestaurantAsync(_mapper.Map<ModifyRestaurantDto>(modifyRestaurantModel));
        return await Edit(modifyRestaurantModel.Id);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _restaurantService.DeleteRestaurantAsync(id);
        return await Index();
    }
}