using QLNS.DTO;
using QLNS.ModelsParameter.SupplyInvoice;

namespace QLNS.Interfaces
{
    public interface ISupplyInvoice
    {
        Task<List<SupplyInvoiceDTO>> GetAllSupplyInvoice();
        Task<bool> CreateSupplyInvoice(CreateSupplyInvoiceRequest request);
        Task<SupplyInvoiceDTO> GetSupplyInvoiceById(int id);
        Task<List<ViewSupply>> ViewSupplies();
    }
}
