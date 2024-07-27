using QLNS.DTO;
using QLNS.ModelsParameter.Cart;

namespace QLNS.Interfaces
{
    public interface ICart
    {
        Task<List<CartDTO>> GetCartsByUsername(string username);
		Task<bool> AddProduct(RequestAddCart request);
		Task<Boolean> CheckExistCart(RequestCheckCart request);
    }
}
