using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportDetailController : ControllerBase
    {
        private readonly IImportDetail _importDetail;
        public ImportDetailController(IImportDetail importDetail)
        {
            _importDetail = importDetail;
        }
        [HttpGet("{id}")]
        public IActionResult GetImportDetailsBySupplyId(int id)
        {
            var res = _importDetail.GetImportDetailsBySupplyId(id);
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
