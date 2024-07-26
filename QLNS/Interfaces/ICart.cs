using QLNS.DTO;
using QLNS.ModelsParameter.Cart;

namespace QLNS.Interfaces
{
    public interface ICart
    {
        List<CartDTO> GetCartsByUsername(string username);
        Boolean AddProduct(RequestAddCart request);
        Boolean CheckExistCart(RequestCheckCart request);
    }
}
