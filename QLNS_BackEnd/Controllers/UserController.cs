using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Admin;
using QLNS_BackEnd.ModelsParameter.User;

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
		[HttpGet("{id}")]
		public IActionResult GetUser(int id) 
		{
            var res = _user.GetUserById(id);
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
		public IActionResult CreateUser(AddUser request)
		{
            var res = _user.CreateUser(request);
            return Ok(res);
        }
		[HttpPost("update")]
        public IActionResult UpdateUser(UpdateUser request)
        {
            var res = _user.UpdateUser(request);
            return Ok(res);
        }
    }
}
