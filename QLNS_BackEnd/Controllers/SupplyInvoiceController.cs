using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.SupplyInvoice;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyInvoiceController : ControllerBase
    {
        private readonly ISupplyInvoice _supplyInvoice;
        public SupplyInvoiceController(ISupplyInvoice supplyInvoice)
        {
            _supplyInvoice = supplyInvoice;
        }
        [HttpGet]
        public IActionResult GetAllSupplyInvoice() 
        {
            var res = _supplyInvoice.GetAllSupplyInvoice();
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
        public IActionResult GetSupplyInvoiceById(int id) 
        {
            var res = _supplyInvoice.GetSupplyInvoiceById(id);
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
        public IActionResult CreateSupplyInvoice(CreateSupplyInvoiceRequest request)
        {
            var res = _supplyInvoice.CreateSupplyInvoice(request);
            return Ok(res);
        }
    }
}
