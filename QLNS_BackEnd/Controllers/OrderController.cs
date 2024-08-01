using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.DTO;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }
        [HttpGet("{username}")]
        public IActionResult GetOrdersByUsername(string username)
        {
            var res = _order.GetOrdersByUsername(username);
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
        public IActionResult GetOrderById(int id)
        {
            var res = _order.GetOrderById(id);
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
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            var res = _order.CreateOrder(request);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult UpDateOrder(OrderDTO request)
        {
            var res = _order.UpDateOrder(request);
            return Ok(res);
        }
    }
}
