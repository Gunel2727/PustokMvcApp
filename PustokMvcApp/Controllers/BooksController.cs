using Microsoft.AspNetCore.Mvc;

namespace PustokMvcApp.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
