using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class ShowSupplyViewModel
    {
        public List<SupplyInvoiceDTO> invoiceList { get; set; }
        public List<AdminDTO> adminList { get; set; }
        public List<ProducerDTO> producerList { get; set; }
    }
}
