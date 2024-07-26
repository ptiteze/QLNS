using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SlideController : ControllerBase
	{
		private readonly ISlide _slide;
		public SlideController(ISlide slide)
		{
			_slide = slide;
		}
		[HttpGet]
		public IActionResult GetAllSlides() 
		{
			var res = _slide.GetAllSlides();
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
