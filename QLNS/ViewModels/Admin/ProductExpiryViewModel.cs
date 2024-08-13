using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class ProductExpiryViewModel
    {
        public Dictionary<ImportDetailDTO, DateOnly> SPT {  get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<CatalogDTO> Catalogs { get; set; } = null;
        public List<SupplyInvoiceDTO> SP { get; set; }
    }
}
