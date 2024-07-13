﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNS.DTO;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.ViewModels.Header;
using QLNS.ViewModels.Product;

namespace QLNS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly ICatalog _catalog;
        private readonly IReview _review;
        public ProductController(IProduct product, ICatalog catalog, IReview review)
        {
            _product = product;
            _catalog = catalog;
            _review = review;
        }
        [Route("Product/")]
        [Route("Product/Index")]
        public IActionResult Index(int? id)
        {
            List<CatalogDTO> catalogs = _catalog.GetAllCatalog();
            List<ProductDTO> products = _product.GetAllProducts();
            var resentProducts = products.OrderByDescending(p => p.Created).Take(4).ToList();
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            if (id.HasValue)
            {
                List<ProductDTO> productsByCatalog = _product.GetProductsByCatalogId(id.Value);
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
        public IActionResult ProductDetail(int id)
        {
            List<ProductDTO> products = _product.GetAllProducts();
            Dictionary<ProductDTO, int> cartLocal = new Dictionary<ProductDTO, int>();
            List<Review> reviews = _review.GetReviewsByProductId(id);
            int sumprice = 0;
            sumprice = SetHeaderData(products, sumprice);
            ProductDTO product = products.Find(p => p.Id == id);
            if(product == null)
            {
                return Redirect("/Home/Error");
            }
            var relatedProducts = products.Where(p => p.CatalogId == product.CatalogId).ToList();
            string nameCatalog = _catalog.GetCatalogById(product.CatalogId).Name;

            ProductDetailViewModel Model = new ProductDetailViewModel()
            {
                Product = product,
                RelatedProducts = relatedProducts,
                Reviews = reviews,
                NameCatalog = nameCatalog
            };
            return View(Model);
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
                    sumprice += (productInCart.Price * Quantity);
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
