using QLNS.DTO;
using QLNS.ModelsParameter.Order;

namespace QLNS.Interfaces
{
    public interface ISale
    {
        public Task<List<SaleDTO>> GetSales();
        public Task<SaleDTO> GetSaleById(int id);
        public Task<List<SaleDetailDTO>> GetSaleDetailById(int id);
        public Task<bool> CreateSale(CreateSaleRequest request);
    }
}
