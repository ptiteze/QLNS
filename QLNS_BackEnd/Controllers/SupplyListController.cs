using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.SupplyList;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyListController : ControllerBase
    {
        private readonly ISupplyList _supplyList;
        public SupplyListController(ISupplyList supplyList)
        {
            _supplyList = supplyList;
        }
        [HttpGet]
        public IActionResult GetAllSupplyList() 
        {
            var res = _supplyList.GetAllSupplyList();
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
        public IActionResult CreateSupplyList(CreateSupplyListRequest request)
        {
            var res = _supplyList.CreateSupplyList(request);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult UpdateSupplyList(CreateSupplyListRequest request) // update Quantity
        {
            var res = _supplyList.UpdateSupplyList(request);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplyList(int id)
        {
            var res = _supplyList.DeleteSupplyList(id);
            return Ok(res);
        }
    }
}
