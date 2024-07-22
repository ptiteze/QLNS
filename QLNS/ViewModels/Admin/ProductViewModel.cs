using QLNS.DTO;
namespace QLNS.ViewModels.Admin
{
    public class ProductViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<CatalogDTO> Catalogs { get; set; }
    }
}
