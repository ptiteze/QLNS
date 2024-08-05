using Microsoft.AspNetCore.Mvc;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Order;
using QLNS.ViewModels.Order;

namespace QLNS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUser _user;
        private readonly IAdmin _admin;
        private readonly ICart _cart;
        private readonly ITransaction _transaction;
        private readonly IProduct _product;
        private readonly IOrder _order;
        private readonly IOrdered _ordered;
        public OrderController(IUser user, IAdmin admin, ICart cart, ITransaction transaction, IProduct product, IOrder order, IOrdered ordered)
        {
            _user = user;
            _admin = admin;
            _cart = cart;
            _transaction = transaction;
            _product = product;
            _order = order;
            _ordered = ordered;
        }

        public async Task<IActionResult> Checkout()
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName==null) return RedirectToAction("Index", "Home");
            TransactionDTO transaction = await _transaction.GetTransactionByUserName(UserName);
            if (transaction == null) transaction = new TransactionDTO();
            List<ProductDTO> products = await _product.GetAllProducts();
            List<CartDTO> carts = await _cart.GetCartsByUsername(UserName);
            List<ShowDetail> shows = new List<ShowDetail>();
            foreach(CartDTO c in carts)
            {
                ProductDTO pr = products.Where(p=>p.Id==c.ProductId).FirstOrDefault();
                ShowDetail showDetail = new ShowDetail()
                {
                    Name = pr.Name,
                    price = pr.Price-(pr.Price*(pr.Discount??0))/100,
                    qty = c.Quantity,
                    unit = pr.Unit,
                };
                shows.Add(showDetail);
            }
            CheckoutViewModel model = new CheckoutViewModel()
            {
                Show = shows,
                Transaction = transaction,
            };
            return View(model);
        }
        public async Task<IActionResult> Buy([FromForm] CreateTransactionRequest request)
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            List<UserDTO> users = await _user.GetUsers();
            UserDTO user = users.Where(u=>u.Username==UserName).FirstOrDefault();
            List<ProductDTO> products = await _product.GetAllProducts();
            List<CartDTO> carts =await _cart.GetCartsByUsername(UserName);
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
            users.Clear();
            CreateOrderRequest requestOrder = new CreateOrderRequest()
            {
                UserName = UserName,
                AdminId = 1,
                Sentdate = request.Created,
                Receiveddate = request.Created,
                Address = request.Address,
                Status = 0,
            };
            int orderId = await _order.CreateOrder(requestOrder);
            if (orderId == 0) {
                ModelState.AddModelError("", "Lỗi Không thể mua hàng.");
                return RedirectToAction("Error", "Home");
            }
            OrderDTO order = await _order.GetOrderById(orderId);
            request.OrderId = orderId;
            bool check = await _transaction.CreateTransaction(request);
            if(!check) RedirectToAction("Error", "Home");
            HttpContext.Session.SetString("cart_local", "");
            HttpContext.Session.SetString("sumprice", "0");
            HttpContext.Session.SetString("length_order", "0");
            return Json(new
            {
                orderId
            });
        }
        public async Task<IActionResult> ShowOrder()
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            List<OrderDTO> orders = await _order.GetOrdersByUsername(UserName);
            if(orders==null) orders = new List<OrderDTO>();
            List<TransactionDTO> transactions = new List<TransactionDTO>();
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
                transactions = transactions,
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
    }
}
