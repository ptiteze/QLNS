using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Order;
using QLNS.ViewModels.Order;
using System;

namespace QLNS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUser _user;
        private readonly IAdmin _admin;
        private readonly ICart _cart;
        private readonly IProduct _product;
        private readonly IOrder _order;
        private readonly IOrdered _ordered;
        private readonly IVnPayment _vnPayment;
        public OrderController(IUser user, IAdmin admin, ICart cart, IProduct product, IOrder order,
            IOrdered ordered, IVnPayment vnPayment)
        {
            _user = user;
            _admin = admin;
            _cart = cart;
            _product = product;
            _order = order;
            _ordered = ordered;
            _vnPayment = vnPayment;
        }

        public async Task<IActionResult> Checkout()
        {
            string UserName = HttpContext.Session.GetString("Username");
            string Id = HttpContext.Session.GetString("id_user");
			int IdUser = 0;
			if (!Id.IsNullOrEmpty())
			{
				IdUser = int.Parse(Id);
			}
			UserDTO info = await _user.GetUserById(IdUser);
            if (UserName==null) return RedirectToAction("Index", "Home");
            if (info == null) return RedirectToAction("Error", "Home");
            List<ProductDTO> products = await _product.GetAllProducts();
            List<CartDTO> carts = await _cart.GetCartsByUserId(IdUser);
            List<ShowDetail> shows = new List<ShowDetail>();
            foreach(CartDTO c in carts)
            {
                ProductDTO pr = products.Where(p=>p.Id==c.ProductId).FirstOrDefault();
                ShowDetail showDetail = new ShowDetail()
                {
                    Name = pr.Name,
                    price = pr.Price-(pr.Price*(pr.Discount))/100,
                    qty = c.Quantity,
                    unit = pr.Unit,
                };
                shows.Add(showDetail);
            }
            CheckoutViewModel model = new CheckoutViewModel()
            {
                Show = shows,
                User = info
            };
            return View(model);
        }
        public async Task<IActionResult> Buy([FromForm] CreateOrderRequest request)
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            string Id = HttpContext.Session.GetString("id_user");
            int IdUser = int.Parse(Id);
            UserDTO user = await _user.GetUserById(IdUser);
            List<ProductDTO> products = await _product.GetAllProducts();
            List<CartDTO> carts =await _cart.GetCartsByUserId(IdUser);
            foreach(CartDTO c in carts)
            {
                if (c.Quantity > products.Where(p => p.Id == c.ProductId).FirstOrDefault().Quantity)
                {
                    return Json(new
                    {
                        error = $"Số lượng hàng mua ({c.Quantity}) vượt quá số lượng trong kho ({c.Quantity})."
                    });
                }
            }
            CreateOrderRequest requestOrder = new CreateOrderRequest()
            {
                UserName = UserName,
                AdminId = 1,
                Sentdate = request.Sentdate,
                Receiveddate = request.Receiveddate,
                Address = request.Address,
                Status = 0,
                Amount = request.Amount,
                Message = request.Message,
                Payment = request.Payment,
                UserMail = request.UserMail,
                UserPhone = request.UserPhone,
                UserId = IdUser,
            };
            int orderId = await _order.CreateOrder(requestOrder);
            if (orderId == 0) {
                ModelState.AddModelError("", "Lỗi Không thể mua hàng.");
                return RedirectToAction("Error", "Home");
            }
            HttpContext.Session.SetString("cart_local", "");
            HttpContext.Session.SetString("sumprice", "0");
            HttpContext.Session.SetString("length_order", "0");
            if(request.Payment.Equals("Thanh toán bằng VNPay"))
            {
                var url = await _vnPayment.CreatePaymentUrl(orderId);
                if (url != null)
                {
                    return Redirect(url);
                }
                
            }
            return Json(new
            {
                orderId
            });
        }
        public async Task<IActionResult> ShowOrder()
        {
            string UserName = HttpContext.Session.GetString("Username");
            string Id = HttpContext.Session.GetString("id_user");
            int IdUser = int.Parse(Id);
            if (UserName == null) return RedirectToAction("Index", "Home");
            List<OrderDTO> orders = await _order.GetOrdersByUserId(IdUser);
            if(orders==null) orders = new List<OrderDTO>();
            Dictionary<OrderDTO, int> order = new Dictionary<OrderDTO, int>();

            foreach (OrderDTO o in orders)
            {
                int sumprice = 0;
                List<OrderedDTO> ordereds = await _ordered.GetOrderedsByOrderId(o.Id);
                if (ordereds == null) ordereds = new List<OrderedDTO>();
                Console.WriteLine(ordereds.Count.ToString());
                foreach (OrderedDTO ordered in ordereds)
                {
                    sumprice += ordered.Price * ordered.Qty;
                    Console.WriteLine(sumprice.ToString());
                }
                order.Add(o, sumprice);
            }
            ShowOrderViewModel model = new ShowOrderViewModel()
            {
                orders = order,
                //transactions = transactions,
            };
            return View(model);
        }
        public async Task<IActionResult> Confirm(int id)
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            OrderDTO order = await _order.GetOrderById(id);
            order.Status = 2;
            bool check = await _order.UpDateOrder(order);
            if (check)
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("ShowOrder");
            }
            else
            {
                HttpContext.Session.SetString("errorMsg", "Nhận đơn không thành công");
                return RedirectToAction("ShowOrder");

            }
        }
        public async Task<IActionResult> RemoveOrder(int id)
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            OrderDTO order = await _order.GetOrderById(id);
            if(order == null) return RedirectToAction("Error", "Home");
            order.Status = 3;
            bool check = await _order.UpDateOrder(order);
            if (check)
            {
                return RedirectToAction("ShowOrder");
            }
            else
            {
                HttpContext.Session.Remove("errorMsg");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
