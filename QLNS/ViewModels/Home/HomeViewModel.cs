using QLNS.DTO;
using QLNS.Models;

namespace QLNS.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<CatalogDTO> Catalogs { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<Boardnew> Boardnews { get; set; }
		public List<ProductDTO> BestSellingProducts { get; set; }
		public List<ProductDTO> RecommendedProducts { get; set; }

	}
}
