using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISale _sale;
        public SaleController(ISale sale)
        {
            _sale = sale;
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            var res = _sale.GetSales();
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
        [HttpGet("sale/{id}")]
        public IActionResult GetSaleById(int id) 
        {
            var res = _sale.GetSaleById(id);
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
        [HttpGet("detail/{id}")]
        public IActionResult GetSaleDetailById(int id) 
        {
            var res = _sale.GetSaleDetailById(id);
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
        public IActionResult CreateSale(CreateSaleRequest request)
        {
            var res = _sale.CreateSale(request);
            return Ok(res);
        }
    }
}
