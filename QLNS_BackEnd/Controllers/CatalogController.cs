using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Catalog;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CatalogController : ControllerBase
	{
		private readonly ICatalog _catalog;
		public CatalogController(ICatalog catalog)
		{
			_catalog = catalog;
		}
		[HttpGet("{id}")]
		public IActionResult GetCatalogById(int id) 
		{
			var res = _catalog.GetCatalogById(id);
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
		public IActionResult GetAllCatalog()
		{
			var res = _catalog.GetAllCatalog();
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
		[HttpPut("{name}")]
		public IActionResult AddCatalog(string name)
		{
			var res = _catalog.AddCatalog(name);
			return Ok(res);
		}
		[HttpPost]
		public IActionResult UpdateCatalog(UpdateCatalogRequest request)
		{
			var res = _catalog.UpdateCatalog(request);
			return Ok(res);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteCatalog(int id)
		{
			var res = _catalog.DeleteCatalog(id);
			return Ok(res);
		}
	}
}
