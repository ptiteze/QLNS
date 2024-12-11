using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                        UserId = 0,
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
            string role = HttpContext.Session.GetString("Role");
            if(!role.IsNullOrEmpty())
            if (role.Equals("staff") || role.Equals("admin"))
            {
                return Json(new
                {
                    error = "Quyền quản lý không thể mua hàng"
                });
            }
            int productid = cartItem.productid;
			int quantity = cartItem.quantity;
            ProductDTO prx = await _product.GetProductById(productid);
            if (prx == null || prx.Status == 0)
            {
                return Json(new
                {
                    error = "Sản phẩm hiện không thể đặt hàng"
                });
            }
            int length_order = 0;
            ProductDTO product = await _product.GetProductById(productid);
            if (quantity > product.Quantity)
            {
                return Json(new
                {
                    error = $"Số lượng hàng mua ({quantity}) vượt quá số lượng trong kho ({product.Quantity})."
                });
            } 
                
			//Console.WriteLine(productid.ToString() + "---" + quantity.ToString());
            string UserName = HttpContext.Session.GetString("Username");
            string Id = HttpContext.Session.GetString("id_user");
			int IdUser = 0;
			if (!Id.IsNullOrEmpty())
			{
				IdUser = int.Parse(Id);
			}
			string cart_local = HttpContext.Session.GetString("cart_local");
            if (UserName.IsNullOrEmpty())
            {
				if (cart_local.IsNullOrEmpty())
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
                        userId = IdUser,
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
                            userId = IdUser,
                            productId = productid,
                            quantity = quantity,
                        };
						_cart.AddProduct(requestAdd);
						if (cart_local.IsNullOrEmpty())
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
        public async Task<IActionResult> AddCart(int productid,int quantity)
        {
            string role = HttpContext.Session.GetString("Role");
            if (!role.IsNullOrEmpty())
                if (role.Equals("staff") || role.Equals("admin"))
            {
                return RedirectToAction("Error", "Home");
            }
                ProductDTO prx = await _product.GetProductById(productid);
            if(prx == null || prx.Status == 0 || prx.Quantity == 0)
            {
                return RedirectToAction("ProductDetail", "Product", new { id = productid });
            }
            int length_order = 0;
            Console.WriteLine(productid.ToString() + "---" + quantity.ToString());
            string UserName = HttpContext.Session.GetString("Username");
            string Id = HttpContext.Session.GetString("id_user");
            int IdUser = 0;
            if (!Id.IsNullOrEmpty())
            {
                IdUser = int.Parse(Id);
			}
            string cart_local = HttpContext.Session.GetString("cart_local");
            if (UserName.IsNullOrEmpty())
            {
                if (cart_local.IsNullOrEmpty())
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
                        return RedirectToAction("ProductDetail", "Product", new {id = productid });
                    }
                    cart_local += "|" + compase;
                    length_order = CountOccurrences(cart_local, "|") + 1;

                    HttpContext.Session.SetString("cart_local", cart_local);
                }
                return RedirectToAction("ProductDetail", "Product", new { id = productid });
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    RequestCheckCart requestCheck = new RequestCheckCart()
                    {
                        userId = IdUser,
                        productId = productid,
                    };
                    if (await _cart.CheckExistCart(requestCheck))
                    {
                        length_order = CountOccurrences(cart_local, "|");
                        return RedirectToAction("ProductDetail", "Product", new { id = productid });
                    }
                    else
                    {
                        RequestAddCart requestAdd = new RequestAddCart()
                        {
                            userId = IdUser,
                            productId = productid,
                            quantity = quantity,
                        };
                        _cart.AddProduct(requestAdd);
                        if (cart_local.IsNullOrEmpty())
                        {
                            cart_local = productid.ToString() + ":" + quantity.ToString();
                            length_order = 1;
                            HttpContext.Session.SetString("cart_local", cart_local);
                        }
                        else
                        {
                            cart_local += "|" + productid.ToString() + ":" + quantity.ToString();
                            length_order = CountOccurrences(cart_local, "|") + 1;
                            HttpContext.Session.SetString("cart_local", cart_local);
                        }
                        return RedirectToAction("ProductDetail", "Product", new { id = productid });
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }
        public async Task<IActionResult> UpdateCart([FromForm] UpdateListCartRequest request)
        {
            if(request== null) return RedirectToAction("Index", "Home");
            string UserName = HttpContext.Session.GetString("Username");
            string Id = HttpContext.Session.GetString("id_user");
			int IdUser = 0;
			if (!Id.IsNullOrEmpty())
			{
				IdUser = int.Parse(Id);
			}
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
                        userId = IdUser,
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
            string Id = HttpContext.Session.GetString("id_user");
			int IdUser = 0;
			if (!Id.IsNullOrEmpty())
			{
				IdUser = int.Parse(Id);
			}
			string cart_local = HttpContext.Session.GetString("cart_local");
            int length_order = int.Parse(HttpContext.Session.GetString("length_order"));
            if (UserName != null)
            {
                RequestRemoveCart requestRemove = new RequestRemoveCart() { 
                    userId = IdUser,
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
            Console.WriteLine(cart_local);
            HttpContext.Session.SetString("cart_local", cart_local);
            length_order--;
            Console.WriteLine(length_order.ToString());
            HttpContext.Session.SetString("length_order", length_order.ToString());
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
                    sumprice += (productInCart.Price - (productInCart.Price * (productInCart.Discount)) / 100) * Quantity;
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
        public async Task<IActionResult> ShowCart(int id)
        {
            string UserName = HttpContext.Session.GetString("Username");
            if (UserName == null) return RedirectToAction("Index", "Home");
            int sumprice = 0;
            List<ProductDTO> products = await _product.GetAllProducts();
            List<OrderedDTO> ordereds = await _ordered.GetOrderedsByOrderId(id);
            if (ordereds == null) ordereds = new List<OrderedDTO>();
            Console.WriteLine(ordereds.Count.ToString());
            foreach (OrderedDTO ordered in ordereds)
            {
                sumprice += ordered.Price * ordered.Qty;
            }
            ShowCartViewModel model = new ShowCartViewModel()
            {
                Ordereds = ordereds,
                Products = products,
                sumPrice = sumprice,
                
            };
            return View(model);
        }
    }
}
