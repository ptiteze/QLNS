using Microsoft.AspNetCore.Mvc;
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

		public HomeController(ILogger<HomeController> logger, ICatalog catalog, IProduct product, ISlide slide, IBoardnew boardnew)
		{
			_logger = logger;
			_icatalog = catalog;
			_product = product;
			_slide = slide;
			_boardnew = boardnew;
		}

		public IActionResult Index()
        {
            List<CatalogDTO> catalogs = _icatalog.GetAllCatalog();
            List<ProductDTO> products = _product.GetAllProducts();
            List<Boardnew> boardnews = _boardnew.GetBoardnews();
            List<Slide> slides = _slide.GetAllSlides();
            int sumprice = 0;
            // slide ViewModel
            var slideViewModel = new SlideViewModel()
            {
                Slides = slides
            };
            ViewBag.SlideData = slideViewModel;
            sumprice = SetHeaderData(products, sumprice);
            HomeViewModel Models = new HomeViewModel()
            {
                Catalogs = catalogs,
                Products = products,
                Boardnews = boardnews
            };
            return View(Models);
        }
        public IActionResult Login()
        {
            List<ProductDTO> products = _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View();
        }
        public IActionResult Boardnew(int id)
		{
            List<ProductDTO> products = _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            List<Boardnew> boardnews = _boardnew.GetBoardnews();
            Boardnew boardnew = _boardnew.GetBoardnewById(id);
            BoardnewViewModel Model = new BoardnewViewModel(){
                ThisBoardnew = boardnew,
                Boardnews = boardnews
            };
			return View(Model);
		}
        public IActionResult Introduce()
        {
            List<ProductDTO> products = _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View();
        }
        public IActionResult Policy()
        {
			List<ProductDTO> products = _product.GetAllProducts();
			int sumprice = 0;
			sumprice = SetHeaderData(products, sumprice);
			return View();
		}
        public IActionResult Contact()
        {
            List<ProductDTO> products = _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View();
        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
            List<ProductDTO> products = _product.GetAllProducts();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        // Header ViewModel
        private int SetHeaderData(List<ProductDTO> products, int sumprice)
        {
            Dictionary<ProductDTO, int> cartLocal = new Dictionary<ProductDTO, int>();
            // Header ViewModel
            string cart_local = HttpContext.Session.GetString("cart_local");
            if (!string.IsNullOrEmpty(cart_local))
            {

                List<string> list_cartLocal = new List<string>(cart_local.Split("|"));
                HttpContext.Session.SetString("length_order", list_cartLocal.Count.ToString());
                foreach (string cart in list_cartLocal)
                {
                    string[] parts = cart.Split(':');
                    int ProductId = int.Parse(parts[0]);
                    int Quantity = int.Parse(parts[1]);
                    ProductDTO productInCart = products.Where(p => p.Id == ProductId).FirstOrDefault();
                    sumprice += (productInCart.Price * Quantity);
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

