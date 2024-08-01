using Microsoft.AspNetCore.Mvc;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.ModelsParameter.Cart;
using QLNS.ViewModels.Cart;
using QLNS.ViewModels.Header;
using System;
namespace QLNS.Controllers
{
    public class CartController : Controller
    {

        private readonly ICart _cart;
        private readonly IProduct _product;
        private readonly IOrder _order;
        private readonly IOrdered _ordered;
        public CartController(ICart cart, IProduct product, IOrder order, IOrdered ordered)
        {
            _cart = cart;
            _product = product;
            _order = order;
            _ordered = ordered;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductDTO> products = await _product.GetAllProducts();
            List<ProductDTO> productsInCart = new List<ProductDTO>();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);

            string cart_local = HttpContext.Session.GetString("cart_local");
            if (!string.IsNullOrEmpty(cart_local))
            {
                List<CartDTO> carts = new List<CartDTO>();
                List<string> list_cartLocal = new List<string>(cart_local.Split("|"));
                foreach (string cart in list_cartLocal)
                {
                    string[] parts = cart.Split(':');
                    int ProductId = int.Parse(parts[0]);
                    int Quantity = int.Parse(parts[1]);
                    ProductDTO productInCart = products.Where(p => p.Id == ProductId).FirstOrDefault();
                    productsInCart.Add(productInCart);
                    CartDTO cartx = new CartDTO()
                    {
                        UserName = null,
                        ProductId = ProductId,
                        Quantity = Quantity,
                    };
                    carts.Add(cartx);
                }

                //List<CartDTO> carts = await _cart.GetCartsByUsername(UserName);
                if (carts == null) return RedirectToAction("Index", "Home");
                CartViewModel Models = new CartViewModel()
                {
                    Carts = carts,
                    Products = productsInCart
                };
                return View(Models);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
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
                    RequestCheckCart requestCheck = new RequestCheckCart()
                    {
                        username = UserName,
                        productId = productid,
                    };
					if (await _cart.CheckExistCart(requestCheck))
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
                        RequestAddCart requestAdd = new RequestAddCart()
                        {
                            username = UserName,
                            productId = productid,
                            quantity = quantity,
                        };
						_cart.AddProduct(requestAdd);
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
        public async Task<IActionResult> UpdateCart([FromForm] UpdateListCartRequest request)
        {
            if(request== null) return RedirectToAction("Index", "Home");
            string UserName = HttpContext.Session.GetString("Username");
            string cart_local = "";
            if (UserName != null)
            {
                foreach (var item in request.Items)
                {
                    Console.WriteLine(item.ProductId.ToString() + "++++" + item.Quantity.ToString());
                    RequestAddCart UpdateRequest = new RequestAddCart()
                    {
                        productId = item.ProductId,
                        quantity = item.Quantity,
                        username = UserName,
                    };
                    bool check = await _cart.AddProduct(UpdateRequest);
                    if (!check)
                    {
                        ModelState.AddModelError("", "Lỗi cập nhật giỏ hàng.");
                        return RedirectToAction("Error", "Home");
                    }
                    if (cart_local == "")
                    {
                        cart_local = item.ProductId.ToString() + ":" + item.Quantity.ToString();
                    }
                    else
                    {
                        string compase = item.ProductId.ToString() + ":" + item.Quantity.ToString();
                        cart_local += "|" + compase;
                    }
                }
                HttpContext.Session.SetString("cart_local", cart_local);
            }
            else
            {
                foreach (var item in request.Items)
                {
                    Console.WriteLine(item.ProductId.ToString() + "++++" + item.Quantity.ToString());
                    if (cart_local == "")
                    {
                        cart_local = item.ProductId.ToString() + ":" + item.Quantity.ToString();
                    }
                    else
                    {
                        string compase = item.ProductId.ToString() + ":" + item.Quantity.ToString();
                        cart_local += "|" + compase;
                    }
                }
                HttpContext.Session.SetString("cart_local", cart_local);
            }
            
            return RedirectToAction("Index", "Cart");
        }
        public async Task<IActionResult> Remove(int id)
        {
            string UserName = HttpContext.Session.GetString("Username");
            string cart_local = HttpContext.Session.GetString("cart_local");
            if (UserName != null)
            {
                RequestRemoveCart requestRemove = new RequestRemoveCart() { 
                    UserName = UserName,
                    ProductId = id,
                };
                bool check = await _cart.RemoveCart(requestRemove);
                if(!check) { return RedirectToAction("Error", "Home"); }
            }
            string substring = id.ToString() + ":";
            List<string> list_cartLocal = new List<string>(cart_local.Split("|"));
            foreach(string e in list_cartLocal)
            {
                if (e.Contains(substring))
                {
                    list_cartLocal.Remove(e);
                    break;
                }
            }
            cart_local = "";
            foreach (string e in list_cartLocal)
            {
                cart_local += e + "|";
            }
            if(cart_local!="") cart_local = cart_local.Remove(cart_local.Length - 1);
            HttpContext.Session.SetString("cart_local", cart_local);
            return RedirectToAction("Index", "Cart");
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
                    sumprice += (productInCart.Price - (productInCart.Price * (productInCart.Discount ?? 0)) / 100) * Quantity;
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
        public async Task<IActionResult> Buy()
        {
            string UserName = HttpContext.Session.GetString("Username");
            if(UserName == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
