using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BoardnewController : ControllerBase
	{
		private readonly IBoardnew _boardnew;
		public BoardnewController(IBoardnew boardnew)
		{
			_boardnew = boardnew;
		}
		[HttpGet]
		public async Task<IActionResult> GetBoardnews()
		{
			var res = await _boardnew.GetBoardnews();
			if(res == null)
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
		[HttpPost("{id}")]
		public IActionResult GetBoardnewById(int id)
		{
			var res = _boardnew.GetBoardnewById(id);
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
	}
}
