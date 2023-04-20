using AutoMapper;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Models;

namespace RestaurantAggregator.BL.Helper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<DishOptions, FetchDishOptions>();
        CreateMap<Dish, DishDto>()
            .ForMember(dto => dto.Rating, options => options.MapFrom(dish =>
                (dish.Reviews == null || !dish.Reviews.Any()) ? (double?)null : dish.Reviews.Average(r => r.Rating))); // todo сделать поле, которое будет уже считать предпросчитанную среднюю оценку, а потом просто менять это поле в методе добавления оценки
    }
}