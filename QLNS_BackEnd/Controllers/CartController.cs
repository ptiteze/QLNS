﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Cart;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICart _cart;
		public CartController(ICart cart)
		{
			_cart = cart;
		}
		[HttpGet("{username}")]
		public ActionResult GetCartsByUsername(string username) 
		{
			var res = _cart.GetCartsByUsername(username);
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
		[HttpPut]
		public IActionResult AddProduct(RequestAddCart request)
		{
			var res = _cart.AddProduct(request);
			return Ok(res);
		}
		[HttpPost]
		public IActionResult CheckExistCart(RequestCheckCart request)
		{
			var res = _cart.CheckExistCart(request);
			return Ok(res);
		}
	}
}