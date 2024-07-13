using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Singleton;

namespace QLNS.Repositories
{
    public class CartRepository : ICart
    {
        public List<CartDTO> GetCartsByUsername(string username)
        {
            return SingletonAutoMapper.GetInstance().Map<List<CartDTO>>(
                SingletonDataBridge.GetInstance().Carts.Where(c => c.UserName == username));
        }
    }
}
