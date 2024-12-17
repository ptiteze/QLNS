using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ViewModels.Header;
using QLNS.ViewModels.Home;
using QLNS.ViewModels.SlideView;
using System.Diagnostics;

namespace QLNS.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICatalog _icatalog;
		private readonly IProduct _product;
		private readonly ISlide _slide;
		private readonly IBoardnew _boardnew;
		private readonly ICart _cart;
        private readonly IRecommendation _recommendation;

		public HomeController(ILogger<HomeController> logger, ICatalog catalog, IProduct product, ISlide slide
            , IBoardnew boardnew, IRecommendation recommendation)
		{
			_logger = logger;
			_icatalog = catalog;
			_product = product;
			_slide = slide;
			_boardnew = boardnew;
            _recommendation = recommendation;
		}

		public async Task<IActionResult> Index()
        {
            try
            {
                List<CatalogDTO> catalogs = await _icatalog.GetAllCatalog();
                List<ProductDTO> products = await _product.GetAllProducts();
                List<ProductDTO> bestsellproduct = await _product.GetBestSellingProducts();
                HashSet<ProductDTO> setPrs = new HashSet<ProductDTO>();
                List<ProductDTO> recommendedProducts = new List<ProductDTO>();
                List<ProductDTO> recommendedProductsByRated = new List<ProductDTO>();
                string Id = HttpContext.Session.GetString("id_user") ?? "";
                int IdUser = 0;
                if (!Id.IsNullOrEmpty())
                {
                    IdUser = int.Parse(Id);
                    recommendedProductsByRated = await _product.GetRecommendedProductsByRated(IdUser);
                }
                List<Boardnew> boardnews = await _boardnew.GetBoardnews();
                List<Slide> slides = await _slide.GetAllSlides();
                List<int> UsedListP = new List<int>();
                string useds = "";
                int sumprice = 0;
                // slide ViewModel
                var slideViewModel = new SlideViewModel()
                {
                    Slides = slides
                };
                ViewBag.SlideData = slideViewModel;
                sumprice = SetHeaderData(products, sumprice);
                string cart_local = HttpContext.Session.GetString("cart_local") ?? "";
                if (!cart_local.IsNullOrEmpty())
                {
                    List<string> list_cartLocal = new List<string>(cart_local.Split("|"));
                    foreach (var item in list_cartLocal)
                    {
                        string[] parts = item.Split(':');
                        int ProductId = int.Parse(parts[0]);
                        UsedListP.Add(ProductId);
                        List<ProductDTO> prs = await _product.GetRecommendedProducts(ProductId);
                        setPrs.UnionWith(prs);
                    }
                    recommendedProducts = setPrs.ToList();
                }
                else
                {
                    recommendedProducts = products;

                }
                if (UsedListP.Count > 0)
                    useds = await _recommendation.GetUseds(UsedListP);
                HomeViewModel Models = new HomeViewModel()
                {
                    Catalogs = catalogs,
                    Products = products,
                    Boardnews = boardnews,
                    BestSellingProducts = bestsellproduct,
                    RecommendedProducts = recommendedProducts,
                    Useds = useds,
                    RecommendedProductsByRated = recommendedProductsByRated,
                };
                return View(Models);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
        public async Task<IActionResult> Login()
        {
            List<ProductDTO> products = await _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View();
        }
        public async Task<IActionResult> Boardnew(int id)
		{
            List<ProductDTO> products = await _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            List<Boardnew> boardnews = await _boardnew.GetBoardnews();
            Boardnew boardnew = await _boardnew.GetBoardnewById(id);
            BoardnewViewModel Model = new BoardnewViewModel(){
                ThisBoardnew = boardnew,
                Boardnews = boardnews
            };
			return View(Model);
		}
        public async Task<IActionResult> Introduce()
        {
            List<ProductDTO> products = await _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View();
        }
        public async Task<IActionResult> Policy()
        {
			List<ProductDTO> products = await _product.GetAllProducts();
			int sumprice = 0;
			sumprice = SetHeaderData(products, sumprice);
			return View();
		}
        public async Task<IActionResult> Guide()
        {
            List<ProductDTO> products = await _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            List<ProductDTO> products = await _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string s)
        {
            if(string.IsNullOrEmpty(s))
                return RedirectToAction("Index");
            List<ProductDTO> products = await _product.GetAllProducts();
            List<CatalogDTO> catalogs = await _icatalog.GetAllCatalog();
            var resentProducts = products.OrderByDescending(p => p.Created).Take(4).ToList();
            List<ProductDTO> filteredProducts = products
       .Where(p => p.Name.Contains(s, StringComparison.OrdinalIgnoreCase))
       .ToList();
            if (filteredProducts.Count == 0) filteredProducts = new List<ProductDTO>();
            SearchViewModel model = new SearchViewModel()
            {
                Catalogs = catalogs,
                Products = filteredProducts,
                RecentProducts = resentProducts,
                search_string = s,
            };
            return View(model);
        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public async Task<IActionResult> Error()
		{
            List<ProductDTO> products = await _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        // Header ViewModel
        private int SetHeaderData(List<ProductDTO> products, int sumprice)
        {
            Dictionary<ProductDTO, int> cartLocal = new Dictionary<ProductDTO, int>();
            // Header ViewModel
            string cart_local = HttpContext.Session.GetString("cart_local") ?? "";
            
            if (!string.IsNullOrEmpty(cart_local))
            {
                Console.WriteLine(cart_local);
                List<string> list_cartLocal = new List<string>(cart_local.Split("|"));
                HttpContext.Session.SetString("length_order", list_cartLocal.Count.ToString());
                foreach (string cart in list_cartLocal)
                {
                    string[] parts = cart.Split(':');
                    int ProductId = int.Parse(parts[0]);
                    int Quantity = int.Parse(parts[1]);
                    ProductDTO productInCart = products.Where(p => p.Id == ProductId).FirstOrDefault() ?? new ProductDTO();
                    if(productInCart!=null)
                    sumprice += (productInCart.Price - (productInCart.Price * (productInCart.Discount)) / 100) * Quantity;

                    cartLocal.Add(productInCart, Quantity);
                }
                var headerViewModel = new HeaderViewModel()
                {
                    CartLocal = cartLocal
                };
                HttpContext.Session.SetString("sumprice", sumprice.ToString());
                ViewBag.HeaderData = headerViewModel;
            }
            else
            {
                HttpContext.Session.SetString("sumprice", sumprice.ToString());
                ViewBag.CartLocal = null;
            }
            return sumprice;
        }
    }
}

