using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.DAL.Repositories.ReviewRepository;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Review>> FetchReviews(Guid dishId)
    {
        return _context.Reviews
            .Where(x => x.Dish.Id == dishId)
            .ToListAsync();
    }

    public Task<bool> IsAnyOrderWithDish(Guid userId, Guid dishId)
    {
        return _context.Orders
            .Where(x => x.UserId == userId)
            .AnyAsync(order => order.DishBaskets
                .Any(dishBasket => dishBasket.Dish.Id == dishId)
            );
    }

    public Task<Review?> FetchReview(Guid dishId, Guid userId)
    {
        return _context.Reviews
            .SingleOrDefaultAsync(x =>
                x.Dish.Id == dishId
                && x.UserId == userId
            );
    }

    public async Task UpdateReview(Review review, int rating)
    {
        review.Rating = rating;
        await _context.SaveChangesAsync();
    }

    public async Task CreateReview(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }
}