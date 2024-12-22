using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class ShowSaleDetailViewModel
    {
        public List<SaleDetailDTO> saleDetails { get; set; }
        public List<ProductDTO> products { get; set; }
        public string nameEmployee { get; set; } = "";
        public SaleDTO sale { get; set; }

    }
}
