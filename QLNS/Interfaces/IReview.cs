using QLNS.Models;

namespace QLNS.Interfaces
{
    public interface IReview
    {
        Task<List<Review>> GetReviewsByProductId(int ProductId);
		Task<Review> GetReviewById(int id);
        Task<List<Review>> GetReviewsByUsername(string username);

    }
}
