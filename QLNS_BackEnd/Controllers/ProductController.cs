﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Cart;
using QLNS_BackEnd.ModelsParameter.Product;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProduct _product;
		public ProductController(IProduct product)
		{
			_product = product;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProducts() {
			var res = await _product.GetAllProducts();
			if (res == null)
			{
				var errorResponse = new
				{
					message = "Không thể lấy dữ liệu",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
		[HttpGet("{id}")]
		public IActionResult GetProductById(int id) {
			var res = _product.GetProductById(id);
			if (res == null)
			{
				var errorResponse = new
				{
					message = "Không thể lấy dữ liệu",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
		[HttpGet("catalog/{id}")]
		public IActionResult GetProductsByCatalogId(int id)
		{
			var res = _product.GetProductsByCatalogId(id);
			if (res == null)
			{
				var errorResponse = new
				{
					message = "Không thể lấy dữ liệu",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var res = _product.DeleteProduct(id);
			return Ok(res);
		}
		[HttpPut("create")]
		public IActionResult CreateProduct(InputProductRequest request)
		{
			var res = _product.AddProduct(request);
			return Ok(res);
		}
		[HttpPost("update")]
		public IActionResult UpdateProduct(UpdateProductRequest request)
		{
			var res = _product.UpdateProduct(request);
			return Ok(res);
		}
		[HttpGet("recommend/{id}")]
		public IActionResult GetRecommendedProducts(int id)
		{
            var res = _product.GetRecommendedProducts(id);
            if (res == null)
            {
                var errorResponse = new
                {
                    message = "Không thể lấy dữ liệu",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }
            return Ok(res);
        }
		[HttpGet("rating_recommend/{id}")]
		public IActionResult GetRecommendedProductsByRated(int id)
		{
			var res = _product.GetRecommendedProductsByRated(id);
			if (res == null)
			{
				var errorResponse = new
				{
					message = "Không thể lấy dữ liệu",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
		[HttpGet("bestsell")]
		public async Task<IActionResult> GetBestsellingProduct()
		{
            var res = await _product.GetProductsBestSelling();
            if (res == null)
            {
                var errorResponse = new
                {
                    message = "Không thể lấy dữ liệu",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }
            return Ok(res);
        }
		[HttpPost("check")]
		public IActionResult CheckPurchase(RequestCheckCart request)
		{
			var res = _product.CheckPurchase(request);
			return Ok(res);
		}
	}
}
