using QLNS_BackEnd.DTO;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Interfaces
{
    public interface ISale
    {
        public List<SaleDTO> GetSales();
        public SaleDTO GetSaleById(int id);
        public List<SaleDetailDTO> GetSaleDetailById(int id);   
        public bool CreateSale(CreateSaleRequest request);
    }
}
