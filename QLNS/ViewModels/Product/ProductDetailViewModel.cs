using QLNS.DTO;
using QLNS.Models;

namespace QLNS.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public List<ProductDTO> RelatedProducts { get; set; }
        public ProductDTO Product { get; set; }
        public List<Review> Reviews { get; set; }
        public string NameCatalog { get; set; }
    }
}
