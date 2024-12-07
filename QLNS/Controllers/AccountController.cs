using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Admin;
using QLNS.ModelsParameter.Cart;
using QLNS.ModelsParameter.User;
using QLNS.ViewModels.Account;

namespace QLNS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _user;
        private readonly ICart _cart;
        private readonly IAccount _account;
        public AccountController(IUser user, ICart cart, IAccount account)
        {
            _user = user;
            _cart = cart;
            _account = account;
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
            //check
            if (username == null || password == null)
            {
                HttpContext.Session.SetString("errorMsg", "Sai cú pháp");
                return RedirectToAction("Login", "Home");
            }
            //check info
            var infoLogin = await _account.Login(request);
            if (infoLogin == null)
            {
                HttpContext.Session.SetString("errorMsg", "Tài khoản/mật khẩu của bạn không đúng");
                return RedirectToAction("Login", "Home");
            }
            if (!infoLogin.Status)
            {
                HttpContext.Session.SetString("errorMsg", "Tài khoản bị khóa");
                return RedirectToAction("Login", "Home");
            }

            // set info
            if (infoLogin.role.Equals("customer"))
            {
                HttpContext.Session.Remove("errorMsg");
                HttpContext.Session.SetString("Username", infoLogin.Username);
                HttpContext.Session.SetString("Fullname", infoLogin.Name);
                HttpContext.Session.SetString("Role", infoLogin.role);
                HttpContext.Session.SetString("id_user", infoLogin.IdUser.ToString());

                string cart_local = HttpContext.Session.GetString("cart_local");
                if (!cart_local.IsNullOrEmpty())
                {
                    List<string> list_cartLocal = new List<string>(cart_local.Split("|"));
                    foreach (string cart in list_cartLocal)
                    {
                        string[] parts = cart.Split(':');
                        int ProductId = int.Parse(parts[0]);
                        int Quantity = int.Parse(parts[1]);
                        RequestCheckCart requestCheck = new RequestCheckCart()
                        {
                            userId = infoLogin.IdUser,
                            productId = ProductId,
                        };

                        RequestAddCart requestAddCart = new RequestAddCart()
                        {
                            productId = ProductId,
                            userId = infoLogin.IdUser,
                            quantity = Quantity,
                        };
                        if (!await _cart.CheckExistCart(requestCheck))
                            await _cart.AddProduct(requestAddCart);
                    }
                }
                List<CartDTO> listCart = await _cart.GetCartsByUserId(infoLogin.IdUser);
                string temp_cart = "";
                if (!listCart.IsNullOrEmpty())
                {
                    foreach (CartDTO cart in listCart)
                    {
                        if (cart != null && cart.ProductId != 0)
                            temp_cart += cart.ProductId.ToString() + ":" + cart.Quantity.ToString() + "|";
                    }
                    if (temp_cart != "") temp_cart = temp_cart.Remove(temp_cart.Length - 1);
                    else return RedirectToAction("Index", "Home");
                    HttpContext.Session.SetString("cart_local", temp_cart);
                    HttpContext.Session.SetString("length_order", listCart.Count.ToString());
                }
            }
            else
            {
                HttpContext.Session.Remove("errorMsg");
                HttpContext.Session.SetString("Username", infoLogin.Username);
                HttpContext.Session.SetString("Fullname", infoLogin.Name);
                HttpContext.Session.SetString("Role", infoLogin.role);
                HttpContext.Session.SetString("id_user", infoLogin.IdUser.ToString());
                return RedirectToAction("Index", "Admin");
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
        public async Task<IActionResult> RegisterResult([FromForm] AddUser request)
        {
            if(request.Phone.Length!=10 || request.Phone[0]!='0')
            {
                return Json(new
                {
                    error = "Có lỗi xảy ra, số điện thoại sai định dạng"
                });
            }    
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
            string Id = HttpContext.Session.GetString("id_user");
            int IdUser = int.Parse(Id);
            if (UserName == null) return RedirectToAction("Index", "Home");
            UserDTO info = await _user.GetUserById(IdUser);
            AccountDTO acc = await _account.GetAccountByUsername(UserName);
            if (info == null || acc == null) return RedirectToAction("Error", "Home");
            InfoAccountViewModel model = new InfoAccountViewModel()
            {
                info = info,
                account = acc
            };
            return View(model);
        }
        public async Task<IActionResult> EditInfo()
        {
            string UserName = HttpContext.Session.GetString("Username");
            string Id = HttpContext.Session.GetString("id_user");
            int IdUser = int.Parse(Id);
            if (UserName == null) return RedirectToAction("Index", "Home");
            UserDTO info = await _user.GetUserById(IdUser);
            AccountDTO acc = await _account.GetAccountByUsername(UserName);
            if(info == null || acc == null) return RedirectToAction("Error", "Home");
            InfoAccountViewModel model = new InfoAccountViewModel() 
            { 
                info = info,
                account = acc
            };
            return View(model);
        }
        public async Task<IActionResult> UpdateAccount([FromForm] UpdateUser request)
        {
            //request.Status = 1;
            if (request.Phone.Length != 10 || request.Phone[0] != '0')
            {
                return Json(new
                {
                    error = "Có lỗi xảy ra, số điện thoại sai định dạng"
                });
            }
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
