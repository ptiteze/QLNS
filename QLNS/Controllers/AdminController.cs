using Microsoft.AspNetCore.Mvc;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ViewModels.Admin;

namespace QLNS.Controllers
{
	public class AdminController : Controller
	{
		private readonly IAdmin _admin;
		private readonly IUser _user;
		private readonly ICatalog _catalog;
		private readonly IProduct _product;
		//private readonly IOrder _order;
		//private readonly IOrdered _ordered;
		private readonly IBoardnew _boardnew;
		public AdminController(IAdmin admin, IUser user, ICatalog catalog, IProduct product, /*IOrder order, IOrdered ordered,*/ IBoardnew boardnew)
		{
			_admin = admin;
			_user = user;
			_catalog = catalog;
			_product = product;
			//_order = order;
			//_ordered = ordered;
			_boardnew = boardnew;
		}
		private bool CheckRole()
		{
            string role = HttpContext.Session.GetString("Role"); ;
            if (role != null && role.Equals("ADMIN")) return true;
			else return false;

        }
		//private readonly 
		public IActionResult Index()
		{
			if(!CheckRole()) return RedirectToAction("Error", "Home");
            return View();
        }
		public IActionResult Admin() 
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<AdminDTO> admins = _admin.GetAdmins();
			AdminViewModel Model = new AdminViewModel(){
			  Admins = admins,
			};
			return View(Model);
		}
		public IActionResult User() 
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<UserDTO> users = _user.GetUsers();
			UserViewModel Model = new UserViewModel() {
				Users = users,
			};
			return View(Model);
		}
		public IActionResult Cate()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<CatalogDTO> catalogs = _catalog.GetAllCatalog();
			CatalogViewModel Model = new CatalogViewModel() 
			{
				Catalogs = catalogs,
			};
			return View(Model);
        }
		public IActionResult Product()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            List<CatalogDTO> catalogs = _catalog.GetAllCatalog();
			List<ProductDTO> products = _product.GetAllProducts();
			ProductViewModel Model = new ProductViewModel() { 
				Catalogs = catalogs,
				Products = products,
			};
			return View(Model);
        }
		public IActionResult News()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<Boardnew> boardnews = _boardnew.GetBoardnews();
			NewsViewModel Model = new NewsViewModel() {
				Boardnews = boardnews,
			};
			return View(Model) ;
        }
	}
}
