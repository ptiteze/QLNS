using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Product;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IReview _review;
		public ReviewController(IReview review)
		{
			_review = review;
		}
		[HttpGet("byproduct/{ProductId}")]
		public IActionResult GetReviewsByProductId(int ProductId) 
		{
			var res = _review.GetReviewsByProductId(ProductId);
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
		public IActionResult GetReviewById(int id)
		{
			var res = _review.GetReviewById(id);
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
		[HttpGet("byuser/{id}")]
		public IActionResult GetReviewsByUserId(int id)
		{
			var res = _review.GetReviewsByUserId(id);
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
		[HttpPost]
		public IActionResult GetReview(InputGetReview input)
		{
            var res = _review.GetReview(input);
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
		public IActionResult CreateReview(CreateReviewRequest request) 
		{
            var res = _review.CreateReview(request);
			return Ok(res);
        }
		[HttpPost("update")]
		public IActionResult UpdateReview(ReviewDTO request)
		{
            var res = _review.UpdateReview(request);
            return Ok(res);
        }
	}
}
