using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Product;

namespace QLNS_BackEnd.Interfaces
{
    public interface IReview
    {
        List<ReviewDTO> GetReviewsByProductId(int ProductId);
        ReviewDTO GetReviewById(int id);
        List<ReviewDTO> GetReviewsByUserId(int id);
        ReviewDTO GetReview(InputGetReview input);
        bool CreateReview(CreateReviewRequest request);
        bool UpdateReview(ReviewDTO request);
    }
}
