using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QLNS.DTO;
using QLNS.Interfaces;

namespace QLNS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _user;
        private readonly IAdmin _admin;
        private readonly ICart _cart;
        public AccountController(IUser user, IAdmin admin, ICart cart)
        {
            _admin = admin; 
            _user = user;
            _cart = cart;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == null || password == null)
            {
                HttpContext.Session.SetString("errorMsg", "Sai cú pháp");
                return RedirectToAction("Login", "Home");
            }
            var infoLogin = _user.Login(username, password);
            if (infoLogin == null)
            {
                infoLogin = _admin.Login(username, password);   
                if(infoLogin == null)
                {
                    HttpContext.Session.SetString("errorMsg", "Tài khoản hoặc mật khẩu của bạn không đúng");
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    if (infoLogin.Status==0)
                    {
                        HttpContext.Session.SetString("errorMsg", "Tài khoản bị khóa");
                        return RedirectToAction("Login", "Home");
                    }
                    HttpContext.Session.Remove("errorMsg");
                    HttpContext.Session.SetString("Username", infoLogin.Username);
                    HttpContext.Session.SetString("Fullname", infoLogin.Name);
                    HttpContext.Session.SetString("Role", infoLogin.role);
                    return RedirectToAction("Index", "Admin");
					//HttpContext.Session.SetString("Cartcount", CartCount.ToString());
				}
            }
            else
            {
                if (infoLogin.Status == 0)
                {
                    HttpContext.Session.SetString("errorMsg", "Tài khoản bị khóa");
                    return RedirectToAction("Login", "Home");
                }
                HttpContext.Session.Remove("errorMsg");
                HttpContext.Session.SetString("Username", infoLogin.Username);
                HttpContext.Session.SetString("Fullname", infoLogin.Name);
                string cart_local = HttpContext.Session.GetString("cart_local");
                if (cart_local != null)
                {
                    List<string> list_cartLocal = new List<string>(cart_local.Split("|"));
                    foreach (string cart in list_cartLocal)
                    {
                        string[] parts = cart.Split(':');
                        int ProductId = int.Parse(parts[0]);
                        int Quantity = int.Parse(parts[1]);
                        if (!_cart.CheckExistCart(infoLogin.Username, ProductId))
                            _cart.AddProduct(infoLogin.Username, ProductId, Quantity);
                    }
                }
                List<CartDTO> listCart = _cart.GetCartsByUsername(username);
                string temp_cart = "";
                foreach(CartDTO cart in listCart)
                {
                    temp_cart += cart.ProductId.ToString() + ":" + cart.Quantity.ToString() + "|";
                }
                if(temp_cart!="") temp_cart = temp_cart.Remove(temp_cart.Length - 1);
                else return RedirectToAction("Index", "Home");
                HttpContext.Session.SetString("cart_local", temp_cart);
            }
            return RedirectToAction("Index","Home");
        }

    }
}
