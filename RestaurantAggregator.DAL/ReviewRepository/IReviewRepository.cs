using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.DAL.ReviewRepository;

public interface IReviewRepository
{
    Task<List<Review>> FetchReviews(Guid dishId);
    
    bool IsAnyOrderWithDish(Guid userId, Guid dishId);
    
    Task<Review?> FetchReview(Guid dishId, Guid userId);
    
    Task UpdateReview(Review review, int ratting);
    
    Task CreateReview(Review review);
}