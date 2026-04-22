using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PustokMvcApp.Services;
using PustokMvcApp.Settings;

namespace PustokMvcApp.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SetCookie()
        {
            Response.Cookies.Append("MyCookie", "Hello from PustokMvcApp");
            return Content("Cookie has been set.");
        }
        public IActionResult GetCookie()
        {
            var cookieValue = Request.Cookies["MyCookie"];
            return Content($"Cookie value: {cookieValue}");
        }
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("MySession", "Hello from PustokMvcApp");
            return Content("Session has been set.");
        }
        public IActionResult GetSession()
        {
            var sessionValue = HttpContext.Session.GetString("MySession");
            return Content($"Session value: {sessionValue}");

        }
    }
}
