using Microsoft.AspNetCore.Mvc;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter;
using QLNS.ViewModels.Cart;
using QLNS.ViewModels.Header;
using System;
namespace QLNS.Controllers
{
    public class CartController : Controller
    {

        private readonly ICart _cart;
        private readonly IProduct _product;
        public CartController(ICart cart, IProduct product)
        {
            _cart = cart;
            _product = product;
        }
        public IActionResult Index()
        {
            string UserName = HttpContext.Session.GetString("Username");
            List<ProductDTO> products = _product.GetAllProducts();
            List<ProductDTO> productsInCart = new List<ProductDTO>();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            List<CartDTO> carts = _cart.GetCartsByUsername(UserName);
            if(carts==null) return RedirectToAction("Index", "Home");
            foreach(CartDTO cart in carts)
            {
                ProductDTO pr = _product.GetProductById(cart.ProductId);
                productsInCart.Add(pr);
            }
            CartViewModel Models = new CartViewModel() 
            { 
                Carts = carts,
                Products = productsInCart
            };
            return View(Models);
        }
        public IActionResult AddToCart([FromBody] CartItem cartItem)
        {
			int productid = cartItem.productid;
			int quantity = cartItem.quantity;
			int length_order = 0;
			Console.WriteLine(productid.ToString() + "---" + quantity.ToString());
            string UserName = HttpContext.Session.GetString("Username");
            string cart_local = HttpContext.Session.GetString("cart_local");
            if (UserName == null)
            {
				if (cart_local == null)
				{
					cart_local = productid.ToString() + ":" + quantity.ToString();
					HttpContext.Session.SetString("cart_local", cart_local);
                    length_order = 1;
				}
				else
				{
					string compase = productid.ToString() + ":" + quantity.ToString();
					if (cart_local.Contains(compase))
					{
                        return Json(new
                        {
                            error = true,
                            message = "Sản phẩm đã có trong danh sách"
                        });
                    }
                    cart_local += "|" + compase;
                    length_order = CountOccurrences(cart_local, "|") +1;

                    HttpContext.Session.SetString("cart_local", cart_local);
				}
				return Json(new
				{
                    length_order
                });
				//return RedirectToAction("Index", "Home");
			}
			else
            {
				try
				{
					if (_cart.CheckExistCart(UserName, productid))
					{
						length_order = CountOccurrences(cart_local, "|");
                        return Json(new
                        {
                            error = true,
                            message = "Sản phẩm đã có trong danh sách"
                        });
                    }
					else
					{
						_cart.AddProduct(UserName, productid, quantity);
						if (cart_local == null)
						{
							cart_local = productid.ToString() + ":" + quantity.ToString();
                            length_order =1;
                            HttpContext.Session.SetString("cart_local", cart_local);
						}
						else
						{
							cart_local += "|" + productid.ToString() + ":" + quantity.ToString();
                            length_order = CountOccurrences(cart_local, "|") + 1;
                            HttpContext.Session.SetString("cart_local", cart_local);
						}
						return Json(new
						{
                            length_order
                        });
					}
				}
				catch (Exception ex)
				{
					// Trả về thông tin lỗi dưới dạng JSON
					return Json(new { error = true, message = ex.Message });
				}
			} 
        }
        static int CountOccurrences(string str, string substring)
        {
            int count = 0;
            int index = 0;

            while ((index = str.IndexOf(substring, index)) != -1)
            {
                count++;
                index += substring.Length;
            }

            return count;
        }
        // Header ViewModel
        private int SetHeaderData(List<ProductDTO> products, int sumprice)
        {
            Dictionary<ProductDTO, int> cartLocal = new Dictionary<ProductDTO, int>();
            // Header ViewModel
            string cart_local = HttpContext.Session.GetString("cart_local");

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
                    ProductDTO productInCart = products.Where(p => p.Id == ProductId).FirstOrDefault();
                    sumprice += ((productInCart.Price - productInCart.Price * productInCart.Discount ?? 0 / 100) * Quantity);
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
