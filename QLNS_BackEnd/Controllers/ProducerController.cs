using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Producer;

namespace QLNS_BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProducerController : ControllerBase
	{
		private readonly IProducer _producer;
		public ProducerController(IProducer producer)
		{
			_producer = producer;
		}
		[HttpGet]
		public IActionResult GetAllProducer()
		{
			var res = _producer.GetAllProducer();
			if (res == null)
			{
				var errorResponse = new
				{
					message = "Không thể lấy dữ liệu.",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
		[HttpGet("{id}")]
		public IActionResult GetProducerById(int id) 
		{
			var res = _producer.GetProducerById(id);
			if (res == null)
			{
				var errorResponse = new
				{
					message = "Không thể lấy dữ liệu.",
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
				};
				return BadRequest(errorResponse);
			}
			return Ok(res);
		}
		[HttpPut]
		public IActionResult CreateProducer(CreateProducerRequest request)
		{
			var res = _producer.CreateProducer(request);
			return Ok(res);
		}
		[HttpPost]
		public IActionResult UpdateProducer(ProducerDTO request)
		{
			var res = _producer.UpdateProducer(request);
			return Ok(res);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteProducer(int id)
		{
			var res = _producer.DeleteProducer(id);
			return Ok(res);
		}
	}
}
