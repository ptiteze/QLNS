using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Cart;

namespace QLNS_BackEnd.Interfaces
{
    public interface ICart
    {
        List<CartDTO> GetCartsByUsername(string username);
        Boolean AddProduct(RequestAddCart request);
        Boolean CheckExistCart(RequestCheckCart request);
    }
}
