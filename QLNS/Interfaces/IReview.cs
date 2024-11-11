using QLNS.DTO;
using QLNS.ModelsParameter.Product;

namespace QLNS.Interfaces
{
    public interface IReview
    {
        Task<List<ReviewDTO>> GetReviewsByProductId(int ProductId);
		Task<ReviewDTO> GetReviewById(int id);
        Task<List<ReviewDTO>> GetReviewsByUserId(int id);
        Task<ReviewDTO> GetReview(InputGetReview input);
        Task<bool> CreateReview(CreateReviewRequest request);
        Task<bool> UpdateReview(ReviewDTO request);
    }
}
