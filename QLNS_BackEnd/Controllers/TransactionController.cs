using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Order;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transaction;
        public TransactionController(ITransaction transaction)
        {
            _transaction = transaction;
        }
        [HttpGet("{username}")]
        public IActionResult GetTransactionByUserName(string username)
        {
            var res = _transaction.GetTransactionByUserName(username);
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
        public IActionResult GetTransactionByOrderId(int id)
        {
            var res = _transaction.GetTransactionByOrderId(id);
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
        public IActionResult CreateTransaction(CreateTransactionRequest request)
        {
            var res = _transaction.CreateTransaction(request);
            return Ok(res);
        }
    }
}
