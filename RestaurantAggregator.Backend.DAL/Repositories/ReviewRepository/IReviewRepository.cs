using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.DAL.Repositories.ReviewRepository;

public interface IReviewRepository
{
    Task<List<Review>> FetchReviews(Guid dishId);
    
    Task<bool> IsAnyOrderWithDish(Guid userId, Guid dishId);
    
    Task<Review?> FetchReview(Guid dishId, Guid userId);
    
    Task UpdateReview(Review review, int ratting);
    
    Task CreateReview(Review review);
}