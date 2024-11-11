using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.ModelsParameter.Product;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class ReviewRepository : IReview
    {
        public bool CreateReview(CreateReviewRequest request)
        {
            try
            {
                Review review = SingletonAutoMapper.GetInstance().Map<Review>(request);
                SingletonDataBridge.GetInstance().Reviews.Add(review);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            
        }

        public ReviewDTO GetReview(InputGetReview input)
        {
            return SingletonAutoMapper.GetInstance().Map<ReviewDTO>(SingletonDataBridge.GetInstance()
                .Reviews.Where(r => r.IdUser == input.UserId && r.ProductId == input.ProductId).FirstOrDefault());
        }

        public ReviewDTO GetReviewById(int id)
        {
            return SingletonAutoMapper.GetInstance().Map<ReviewDTO>(SingletonDataBridge.GetInstance().Reviews.Find(id));
        }

        public List<ReviewDTO> GetReviewsByProductId(int ProductId)
        {
            return SingletonAutoMapper.GetInstance().Map<List<ReviewDTO>>(SingletonDataBridge.GetInstance().Reviews.Where(r =>  r.ProductId == ProductId).ToList());
        }

        public List<ReviewDTO> GetReviewsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateReview(ReviewDTO request)
        {
            try
            {
                //Review review = SingletonAutoMapper.GetInstance().Map<Review>(request);
                Review review = SingletonDataBridge.GetInstance().Reviews.Find(request.Id);
                review.Score = request.Score;
                SingletonDataBridge.GetInstance().Reviews.Update(review);
                SingletonDataBridge.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
