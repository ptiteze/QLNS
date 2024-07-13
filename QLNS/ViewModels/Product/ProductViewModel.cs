using QLNS.DTO;

namespace QLNS.ViewModels.Product
{
    public class ProductViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<ProductDTO> RecentProducts { get; set; }
        public List<CatalogDTO> Catalogs { get; set; }
    }
}
