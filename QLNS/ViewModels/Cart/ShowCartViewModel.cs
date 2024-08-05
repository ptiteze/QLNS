using QLNS.DTO;

namespace QLNS.ViewModels.Cart
{
    public class ShowCartViewModel
    {
        public List<OrderedDTO> Ordereds { get; set; }
        public List<ProductDTO> Products { get; set; }
        public int sumPrice { get; set; }
    }
}
