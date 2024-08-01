using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class ImportDetailViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<ImportDetailDTO> Imports { get; set; }
        public SupplyInvoiceDTO SupplyInvoice { get; set; }
        public ProducerDTO Producer { get; set; }
        public AdminDTO Admin { get; set; }
    }
}
