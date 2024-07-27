using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Admin;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdmin _admin;
		public AdminController(IAdmin admin)
		{
			_admin = admin;
		}
		[HttpGet]
		public IActionResult GetAdmins()
		{
			return Ok(_admin.GetAdmins());
		}
		[HttpPost("login")]
		public IActionResult Login(RequestLogin request)
		{
			var res = _admin.Login(request);
			if (res == null)
			{
				var errorResponse = new
				{
					message = "Login failed. Invalid credentials.",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
		[HttpPost("check")]
		public IActionResult CheckExits(RequestCheckAdmin request)
		{
			var res = _admin.CheckExits(request);
			return Ok(res);
		}
		[HttpPut("create")]
		public IActionResult CreateAdmin(AddAdmin request)
		{
			var res = _admin.CreateAdmin(request);
			return Ok(res);
		}
		[HttpPut("update")]
		public IActionResult UpdateAdmin(UpdateAdmin request)
		{
			var res = (_admin.UpdateAdmin(request));
			return Ok(res);
		}
		[HttpPut("unlock/{id}")]
		public IActionResult UnLockAdmin(int id)
		{
			var res = _admin.UnLockAdmin(id);
			return Ok(res);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteAdmin(int id) 
		{
			var res = _admin.DeleteAdmin(id);
			return Ok(res);
		}
		[HttpGet("get/{id}")]
		public IActionResult GetAdmin(int  id)
		{
			var res = _admin.GetAdmin(id);
			if(res == null)
			{
				var errorResponse = new
				{
					message = "Login failed. Invalid credentials.",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
	}
}
