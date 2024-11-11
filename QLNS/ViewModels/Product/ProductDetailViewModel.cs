using QLNS.DTO;
using QLNS.Models;

namespace QLNS.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public List<ProductDTO> RelatedProducts { get; set; }
        public ProductDTO Product { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
        public string NameCatalog { get; set; }
        public ReviewDTO YourReview { get; set; }
    }
}
