using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ModelsParameter.Cart;
using QLNS.ModelsParameter.Product;
using QLNS.ViewModels.Header;
using QLNS.ViewModels.Product;
using System.Data.SqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QLNS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly ICatalog _catalog;
        private readonly IReview _review;
        private readonly IRecommendation _recommendation;
        public ProductController(IProduct product, ICatalog catalog, IReview review, IRecommendation recommendation)
        {
            _product = product;
            _catalog = catalog;
            _review = review;
            _recommendation = recommendation;
        }
        [HttpGet("Product/")]
        [HttpGet("Product/Index")]
        public async Task<IActionResult> Index(int? id)
        {
            List<CatalogDTO> catalogs = await _catalog.GetAllCatalog();
            List<ProductDTO> products = await _product.GetAllProducts();
			//List<ProductDTO> prs = await _product.GetAllProducts();
			var resentProducts = products.OrderByDescending(p => p.Created).Take(4).ToList();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            if (id.HasValue)
            {
                List<ProductDTO> productsByCatalog = await _product.GetProductsByCatalogId(id.Value);
                if (productsByCatalog.IsNullOrEmpty())
                {
                    return Redirect("/Home/Error");
                }
                ProductViewModel Model = new ProductViewModel()
                {
                    Products = productsByCatalog,
                    RecentProducts = resentProducts,
                    Catalogs = catalogs
                };
                return View(Model);
            }
            else
            {
                Console.WriteLine("index none id");
                ProductViewModel Model = new ProductViewModel()
                {
                    Products = products,
                    RecentProducts = resentProducts,
                    Catalogs = catalogs
                };
                return View(Model);
            }
            
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            Console.WriteLine(id.ToString());
            List<ProductDTO> products = await _product.GetAllProducts();
			List<ProductDTO> Recommendedproducts = await _product.GetRecommendedProducts(id);
            Dictionary<ProductDTO, int> cartLocal = new Dictionary<ProductDTO, int>();
            List<ReviewDTO> reviews = await _review.GetReviewsByProductId(id);
            ReviewDTO review = null;
            string Id = HttpContext.Session.GetString("id_user");
            int IdUser = 0;
            if (!Id.IsNullOrEmpty())
            {
                IdUser = int.Parse(Id);
                InputGetReview input = new InputGetReview() 
                {
                    ProductId = id,
                    UserId = IdUser,
                };
                review = await _review.GetReview(input);
            }
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            ProductDTO product = await _product.GetProductById(id);
            Console.WriteLine(products.Count().ToString());
            if (product == null)
            {
                return Redirect("/Home/Error");
            }
            CatalogDTO cata = await _catalog.GetCatalogById(product.CatalogId);
			string nameCatalog = cata.Name;

            ProductDetailViewModel Model = new ProductDetailViewModel()
            {
                Product = product,
                RelatedProducts = Recommendedproducts,
                Reviews = reviews,
                NameCatalog = nameCatalog,
                YourReview = review,
            };
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Rating(Rating request)
        {
            string Id = HttpContext.Session.GetString("id_user");
            Console.WriteLine(request.product.ToString()+"   abc");
            int IdUser = 0;
            if (!Id.IsNullOrEmpty())
            {
                IdUser = int.Parse(Id);
            }
            else
            {
                return Json(new
                {
                    error = true,
                    message = "Chưa đăng nhập không thể Rating!!!"
                });
            }
            RequestCheckCart requestCheckPurchase = new RequestCheckCart()
            {
                productId = request.product,
                userId = IdUser,
            };
            bool CheckPurchase = await _product.CheckPurchase(requestCheckPurchase);
            if (!CheckPurchase)
            {
                return Json(new
                {
                    error = true,
                    message = "Bạn chưa mua hàng, không thể đánh giá sản phẩm!!"
                });
            }
            InputGetReview input = new InputGetReview() 
            {
                ProductId = request.product,
                UserId = IdUser,
            };
            ReviewDTO review = await _review.GetReview(input);
            try
            {
                if (review != null)
                {
                    review.Score = request.rating;
                    bool check = await _review.UpdateReview(review);
                    if(check)
                    {
                        //await _recommendation.BuildDataset();
                        return Json(new
                        {
                            success = true,
                            message = "Đánh giá sản phẩm thành công!!"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            error = true,
                            message = "Có lỗi xảy ra, không thể sửa đánh giá!!"
                        });
                    }
                }
                else
                {
                    CreateReviewRequest reviewRequest = new CreateReviewRequest()
                    {
                        ContentReview = "Đã sử dụng sản phẩm.",
                        Created = DateOnly.FromDateTime(DateTime.Now),
                        IdUser = IdUser,
                        ProductId = request.product,
                        Score = request.rating,
                    };
                    bool check = await _review.CreateReview(reviewRequest);
                    if (check)
                    {
                        return Json(new
                        {
                            success = true,
                            message = "Đánh giá sản phẩm thành công!!"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            error = true,
                            message = "Có lỗi xảy ra, Không thể tạo đánh giá!!"
                        });
                    }
                }
            }catch (Exception ex)
            {
                return Json(new
                {
                    error = true,
                    message = "Có lỗi xảy ra!!"
                });
            }
            
        }
        // Header ViewModel
        private int SetHeaderData(List<ProductDTO> products, int sumprice)
        {
            Dictionary<ProductDTO, int> cartLocal = new Dictionary<ProductDTO, int>();
            // Header ViewModel
            string cart_local = HttpContext.Session.GetString("cart_local");
            if (!string.IsNullOrEmpty(cart_local))
            {

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
    }
}
