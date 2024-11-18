using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Product;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsedController : ControllerBase
    {
        private readonly IUsed _used;
        public UsedController(IUsed used) 
        {
            _used = used;   
        }
        [HttpGet]
        public IActionResult GetAllUseds()
        {
            var res = _used.GetAllUseds();
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
        public IActionResult GetUsedById(int id) 
        {
            var res = _used.GetUsedById(id);
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
        [HttpGet("product/{id}")]
        public IActionResult GetUsedByProduct(int id)
        {
            var res = _used.GetUsedsByProduct(id);
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
        public IActionResult CreateUsedProduct(UsedProductRequest request)
        {
            var res = _used.CreateUsedProduct(request);
            return Ok(res);
        }
        [HttpPost("detele")]
        public IActionResult DeleteUsedProduct(UsedProductRequest request)
        {
            var res = _used.DeleteUsedProduct(request);
            return Ok(res);
        }
        [HttpGet("create/{usedname}")]
        public IActionResult CreateUsed(string usedname)
        {
            var res = _used.CreateUsed(usedname);
            return Ok(res);
        }
        [HttpDelete("used/{id}")]
        public IActionResult DeleteUsed(int id)
        {
            var res = _used.DeleteUsed(id);
            return Ok(res);
        }
    }
}
