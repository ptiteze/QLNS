using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Models;
using QLNS_BackEnd.Singleton;

namespace QLNS_BackEnd.Repositories
{
    public class ReviewRepository : IReview
    {
        public Review GetReviewById(int id)
        {
            return SingletonDataBridge.GetInstance().Reviews.Find(id);
        }

        public List<Review> GetReviewsByProductId(int ProductId)
        {
            return SingletonDataBridge.GetInstance().Reviews.Where(r =>  r.ProductId == ProductId).ToList();
        }

        public List<Review> GetReviewsByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
