using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class SupplyInvoiceViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<SupplyListDTO> SupplyList { get; set; }
        public List<ProducerDTO> Producers { get; set; }
    }
}
