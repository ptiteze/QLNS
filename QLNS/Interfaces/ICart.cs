using QLNS.DTO;

namespace QLNS.Interfaces
{
    public interface ICart
    {
        List<CartDTO> GetCartsByUsername(string username);
        Boolean AddProduct(string username, int productId, int quantity);
        Boolean CheckExistCart(string username, int productId);
    }
}
