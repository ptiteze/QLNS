using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Admin;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUser _user;
		public UserController(IUser user) 
		{ 
			_user = user;
		}

		[HttpPost]
		public IActionResult Login(RequestLogin request)
		{
			var res = _user.Login(request);
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
		[HttpGet]
		public IActionResult GetUsers() {
			var res = _user.GetUsers();
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
		[HttpPost("lock")]
		public IActionResult LockUser(string username)
		{
			var res = _user.LockUser(username);
			return Ok(res);
		}
		[HttpPost("unlock")]
		public IActionResult UnLockUser(string username)
		{
			var res = _user.UnLockUser(username);
			return Ok(res);
		}
	}
}
