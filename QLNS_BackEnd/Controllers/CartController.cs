using Microsoft.AspNetCore.Http;
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
		[HttpGet("{id}")]
		public ActionResult GetCartsByUserid(int id) 
		{
			var res = _cart.GetCartsByUserId(id);
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
		[HttpPost("remove")]
		public IActionResult RemoveCart(RequestRemoveCart request)
		{
            var res = _cart.RemoveCart(request);
            return Ok(res);
        }
    }
}
