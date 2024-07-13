using QLNS.Interfaces;
using QLNS.Models;
using QLNS.Singleton;

namespace QLNS.Repositories
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
