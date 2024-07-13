using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QLNS.DTO;
using QLNS.Interfaces;

namespace QLNS.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == null || password == null)
            {
                HttpContext.Session.SetString("errorMsg", "Sai cú pháp");
                return View("Login");
            }
            return View();
        }

    }
}
