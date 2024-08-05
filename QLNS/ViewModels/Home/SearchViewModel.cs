using QLNS.DTO;

namespace QLNS.ViewModels.Home
{
    public class SearchViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public string search_string { get; set; }
        public List<ProductDTO> RecentProducts { get; set; }
        public List<CatalogDTO> Catalogs { get; set; }
    }
}
