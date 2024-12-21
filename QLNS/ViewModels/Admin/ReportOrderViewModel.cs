using QLNS.DTO;
using QLNS.ModelsParameter.Order;

namespace QLNS.ViewModels.Admin
{
    public class ReportOrderViewModel
    {
        public List<ReportData> reportData { get; set; } = null;
        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime endDate { get; set; } = DateTime.Now;
        public Decimal value { get; set; } = 0;
        public List<CatalogDTO> catalogList { get; set; }
        public List<ProductDTO> productList { get; set; }
    }
}
