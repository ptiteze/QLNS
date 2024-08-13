using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class ProductInWeekViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public Dictionary<int, int> quantity { get; set; } 
        public List<CatalogDTO> Catalogs { get; set; } = null;
        public List<SupplyListDTO> SupplyList { get; set; }
    }
}
