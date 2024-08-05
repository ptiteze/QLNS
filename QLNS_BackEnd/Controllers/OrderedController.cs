using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderedController : ControllerBase
    {
        private readonly IOrdered _ordered;
        public OrderedController(IOrdered ordered)
        {
            _ordered = ordered;
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderedsByOrderId(int id)
        {
            var res = _ordered.GetOrderedsByOrderId(id);
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
