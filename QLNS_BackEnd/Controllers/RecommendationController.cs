using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecommendationController : ControllerBase
	{
		private readonly IRecommend _recommendation;
		public RecommendationController(IRecommend recommendation)
		{
			_recommendation = recommendation;
		}
		[HttpPost]
		public IActionResult GetUseds(List<int> listProduct)
		{
			var res = _recommendation.GetUseds(listProduct);
			return Content(res, "application/json");
		}
		[HttpGet]
		public IActionResult BuildDataset()
		{
			var res = _recommendation.BuildDataset();
			return Ok(res);
		}
	}
}
