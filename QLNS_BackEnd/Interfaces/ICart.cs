using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Cart;

namespace QLNS_BackEnd.Interfaces
{
    public interface ICart
    {
        List<CartDTO> GetCartsByUserId(int id);
        Boolean AddProduct(RequestAddCart request);
        Boolean CheckExistCart(RequestCheckCart request);
        bool RemoveCart(RequestRemoveCart request);
    }
}
