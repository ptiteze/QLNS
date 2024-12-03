using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Cart;

namespace QLNS_BackEnd.Interfaces
{
    public interface ICart
    {
        Task<List<CartDTO>> GetCartsByUserId(int id);
        Task<Boolean> AddProduct(RequestAddCart request);
        Task<Boolean> CheckExistCart(RequestCheckCart request);
        Task<bool> RemoveCart(RequestRemoveCart request);
    }
}
