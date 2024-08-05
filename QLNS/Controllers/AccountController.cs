using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Admin;
using QLNS.ModelsParameter.Cart;
using QLNS.ViewModels.Account;

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
        public async Task<IActionResult> Login(string username, string password)
        {
            RequestLogin request = new RequestLogin()
            {
                username = username,
                password = password,
            };
            if (username == null || password == null)
            {
                HttpContext.Session.SetString("errorMsg", "Sai cú pháp");
                return RedirectToAction("Login", "Home");
            }
            var infoLogin = await _user.Login(request);
            if (infoLogin == null)
            {
                infoLogin = await _admin.Login(request);   
                if(infoLogin == null)
                {
                    HttpContext.Session.SetString("errorMsg", "Tài khoản/mật khẩu của bạn không đúng");
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
                        RequestCheckCart requestCheck = new RequestCheckCart() {
                            username = infoLogin.Username,  
                            productId = ProductId,
                        };

						RequestAddCart requestAddCart = new RequestAddCart()
				        {
				        	productId = ProductId,
                            username = infoLogin.Username,
                            quantity = Quantity,
						};
						if (!await _cart.CheckExistCart(requestCheck))
                            _cart.AddProduct(requestAddCart);
                    }
                }
                List<CartDTO> listCart = await _cart.GetCartsByUsername(username);
                string temp_cart = "";

                foreach(CartDTO cart in listCart)
                {
                    if(cart!=null && cart.ProductId!=0)
                    temp_cart += cart.ProductId.ToString() + ":" + cart.Quantity.ToString() + "|";
                }
                if(temp_cart!="") temp_cart = temp_cart.Remove(temp_cart.Length - 1);
                else return RedirectToAction("Index", "Home");
                HttpContext.Session.SetString("cart_local", temp_cart);
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> RegisterResult([FromForm] UserDTO request)
        {
            bool check = await _user.CreateUser(request);
            if(check)
            {
                return Json(new
                {
                    check
                });
            }
            else
            {
                return Json(new
                {
                    error = "Có lỗi xảy ra, thông tin bị trùng"
                });
            }
        }
        public async Task<IActionResult> Info()
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            List<UserDTO> users = await _user.GetUsers();
            UserDTO info = users.Where(u => u.Username == UserName).FirstOrDefault();
            InfoAccountViewModel model = new InfoAccountViewModel() { info = info };
            return View(model);
        }
        public async Task<IActionResult> EditInfo()
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            List<UserDTO> users = await _user.GetUsers();
            UserDTO info = users.Where(u => u.Username == UserName).FirstOrDefault();
            InfoAccountViewModel model = new InfoAccountViewModel() { info = info };
            return View(model);
        }
        public async Task<IActionResult> UpdateAccount([FromForm] UserDTO request)
        {
            //request.Status = 1;
            bool check = await _user.UpdateUser(request);
            if (check)
            {
                return Json(new
                {
                    check
                });
            }
            else
            {
                return Json(new
                {
                    error = "Có lỗi xảy ra, thông tin bị trùng"
                });
            }
        }
    }
}
