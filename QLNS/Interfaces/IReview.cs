using QLNS.Models;

namespace QLNS.Interfaces
{
    public interface IReview
    {
        List<Review> GetReviewsByProductId(int ProductId);
        Review GetReviewById(int id);
        List<Review> GetReviewsByUsername(string username);

    }
}
