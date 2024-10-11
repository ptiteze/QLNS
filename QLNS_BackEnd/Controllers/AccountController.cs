using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.ModelsParameter.Admin;

namespace QLNS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount  _account;
        public AccountController(IAccount account)
        {
            _account = account;
        }
        [HttpPost]
        public IActionResult Login(RequestLogin request)
        {
            var res = _account.Login(request);
            if (res == null)
            {
                var errorResponse = new
                {
                    message = "Login failed. Invalid credentials.",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }
            return Ok(res);
        }
        [HttpGet]
        public IActionResult GetAccounts() 
        {
            var res = _account.GetAccounts();
            if (res == null)
            {
                var errorResponse = new
                {
                    message = "Invalid credentials.",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }
            return Ok(res);
        }
        [HttpGet("check/{username}")]
        public IActionResult Check(string username) 
        {
            var res = _account.CheckExitsUsermane(username);
            return Ok(res);
        }
        [HttpGet("{username}")]
        public IActionResult GetAccoutByUsername(string username)
        {
            var res = _account.GetAccountByUsername(username);
            if (res == null)
            {
                var errorResponse = new
                {
                    message = "Invalid credentials.",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }
            return Ok(res);
        }
        [HttpGet("lock/{id}")]
        public IActionResult LockAccount(int id)
        {
            var res = _account.LockAccount(id);
            return Ok(res);
        }

        [HttpGet("unlock/{id}")]
        public IActionResult UnlockAccount(int id)
        {
            var res = _account.UnlockAccount(id);
            return Ok(res);
        }
    }
}
