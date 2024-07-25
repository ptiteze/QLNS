using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter;
using QLNS.ViewModels.Admin;
using System.Text.RegularExpressions;

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
		// Admin 
		public IActionResult AddAdmin()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            return View();
		}
		public IActionResult AddAdminResult(string? admin_username, string? admin_password, string? admin_name,
			string? admin_email, string? admin_phone)
		{
			if(admin_username.IsNullOrEmpty()|| admin_password.IsNullOrEmpty()|| admin_name.IsNullOrEmpty())
			{
                HttpContext.Session.SetString("errorMsg", "Không đươc bỏ trống ô");
				return RedirectToAction("AddAdmin", "Admin");
            }
			if(!admin_email.Contains("@")|| admin_email.IsNullOrEmpty())
			{
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("AddAdmin", "Admin");
            }
			if(admin_phone.IsNullOrEmpty() || !Regex.IsMatch(admin_phone, @"^[\d\.\-\+]+$"))
			{
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("AddAdmin", "Admin");
            }
			if(_admin.CheckExits(admin_username, admin_email, admin_phone))
			{
                HttpContext.Session.SetString("errorMsg", "Các thông tin về username hoặc email, số điện thoại bị trùng");
                return RedirectToAction("AddAdmin", "Admin");
            }
			AddAdmin request = new AddAdmin() { 
				Username = admin_username,
				Email = admin_email,
				Name = admin_name,
				Status = 1,
				Password = admin_password,	
				Phone = admin_phone,
			};
			bool check = _admin.CreateAdmin(request);
			if(check)
			{
				HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");	
			}
			else
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra, hãy thử lại");
                return RedirectToAction("AddAdmin", "Admin");
            }
		}
		public IActionResult DeleteAdmin(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = _admin.DeleteAdmin(id);
			if(check)
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");
            }
			else
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Admin", "Admin");
            }
		}
		public IActionResult EditAdmin(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            AdminDTO admin = _admin.GetAdmin(id);
			EditAdminViewModel Model = new EditAdminViewModel() { 
			Admin = admin,
			};
			return View(Model);
		}
		public IActionResult EditAdminResult(int id, string? password, string? name, string? phone, string? email)
		{
            if (password.IsNullOrEmpty() || name.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("errorMsg", "Không đươc bỏ trống ô");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
            if (!email.Contains("@") || email.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
            if (phone.IsNullOrEmpty() || !Regex.IsMatch(phone, @"^[\d\.\-\+]+$"))
            {
                HttpContext.Session.SetString("errorMsg", "Email sai cú pháp");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
			UpdateAdmin request = new UpdateAdmin() 
			{ 
				Id = id,
				Name = name, 
				Password = password,
				Phone = phone,
				Email = email,
			};
			bool check = _admin.UpdateAdmin(request);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra, hãy thử lại");
                return RedirectToAction("EditAdmin", "Admin", new { id = id });
            }
        }
		public IActionResult UnLockAdmin(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = _admin.UnLockAdmin(id);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Admin", "Admin");
            }
        }
		//User
		public IActionResult LockUser(string username)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = _user.LockUser(username);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("User", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("User", "Admin");
            }
        }
        public IActionResult UnLockUser(string username)
        {
            if (!CheckRole()) return RedirectToAction("Error", "Home");
            bool check = _user.UnLockUser(username);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("User", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("User", "Admin");
            }
        }
		// Catalog
		public IActionResult AddCate()
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			return View();
        }
		public IActionResult AddCateResult(string? name)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<CatalogDTO> catalogs = _catalog.GetAllCatalog();
			if(name == null)
			{
                HttpContext.Session.SetString("errorMsg", "Không được bỏ trống thông tin");
                return RedirectToAction("AddCate", "Admin");
            }
			foreach(CatalogDTO item in catalogs) 
			{
				if(item.Name.ToLower() == name.ToLower())
				{
                    HttpContext.Session.SetString("errorMsg", "Thông tin bị trùng");
                    return RedirectToAction("AddCate", "Admin");
                }
			}
			bool check = _catalog.AddCatalog(name);
			if (check)
			{
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Cate", "Admin");
			}
			else
			{
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("AddCate", "Admin");
            }
        }
		public IActionResult RemoveCate(int id)
		{
            if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<ProductDTO> list = _product.GetProductsByCatalogId(id);
			if (list.Count != 0)
			{
                HttpContext.Session.SetString("errorMsg", "Không thể xóa");
                return RedirectToAction("Cate", "Admin");
            }
			bool check = _catalog.DeleteCatalog(id);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Cate", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
                return RedirectToAction("Cate", "Admin");
            }

        }
		public IActionResult EditCate(int id)	
		{
			if (!CheckRole()) return RedirectToAction("Error", "Home");
			CatalogDTO catalog = _catalog.GetCatalogById(id);
			EditCatalogViewModel model = new EditCatalogViewModel() 
			{
				Catalog = catalog,
			};
			return View(model);
		}
		public IActionResult EditCateResult(int id, string name)
		{
			if (!CheckRole()) return RedirectToAction("Error", "Home");
			List<CatalogDTO> catalogs = _catalog.GetAllCatalog();
			catalogs.RemoveAll(c => c.Id == id);
			if (name == null)
			{
				HttpContext.Session.SetString("errorMsg", "Không được bỏ trống thông tin");
				return RedirectToAction("AddCate", "Admin");
			}
			foreach (CatalogDTO item in catalogs)
			{
				if (item.Name.ToLower() == name.ToLower())
				{
					HttpContext.Session.SetString("errorMsg", "Thông tin bị trùng");
					return RedirectToAction("AddCate", "Admin");
				}
			}
			bool check = _catalog.UpdateCatalog(id, name);
			if (check)
			{
				HttpContext.Session.Remove("errorMsg");
				return RedirectToAction("Cate", "Admin");
			}
			else
			{
				HttpContext.Session.SetString("errorMsg", "Có lỗi xảy ra");
				return RedirectToAction("EditCate", "Admin");
			}
		}
    }
}
