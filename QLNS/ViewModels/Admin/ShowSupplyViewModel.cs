using QLNS.DTO;
using QLNS.ModelsParameter.SupplyInvoice;

namespace QLNS.ViewModels.Admin
{
    public class ShowSupplyViewModel
    {
        public List<ViewSupply> invoiceList { get; set; }
        public List<AdminDTO> adminList { get; set; }
        public List<ProducerDTO> producerList { get; set; }
        public List<CatalogDTO> catalogList { get; set; }
        public List<ProductDTO> productList { get; set; }
    }
}
