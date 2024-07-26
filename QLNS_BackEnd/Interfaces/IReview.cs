using QLNS_BackEnd.Models;

namespace QLNS_BackEnd.Interfaces
{
    public interface IReview
    {
        List<Review> GetReviewsByProductId(int ProductId);
        Review GetReviewById(int id);
        List<Review> GetReviewsByUsername(string username);

    }
}
