using QLNS.DTO;

namespace QLNS.ViewModels.Cart
{
    public class CartViewModel
    {
        public List<CartDTO> Carts { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
