using QLNS.DTO;

namespace QLNS.ViewModels.Admin
{
    public class EditProductViewModel
    {
        public ProductDTO Product { get; set; }
        public List<CatalogDTO> Catalogs { get; set; }
        public List<UsedDTO> Useds { get; set; } 
        public List<UsedDTO> UsedsOfProduct { get; set; }
    }
}
