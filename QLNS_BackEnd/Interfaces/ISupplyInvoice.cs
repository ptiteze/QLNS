
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.SupplyInvoice;

namespace QLNS_BackEnd.Interfaces
{
    public interface ISupplyInvoice
    {
        List<SupplyInvoiceDTO> GetAllSupplyInvoice();
        bool CreateSupplyInvoice(CreateSupplyInvoiceRequest request);
        SupplyInvoiceDTO GetSupplyInvoiceById(int id);
    }
}
