using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
using PustokMvcApp.Models;
using PustokMvcApp.ViewModels;

namespace PustokMvcApp.Controllers
{
    public class HomeController(PustokAppDbContext pustokAppDbContext)  : Controller
    {
       
        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = pustokAppDbContext.Sliders.ToList(),

                FeaturedBooks = pustokAppDbContext.Books
           .Include(b => b.BookImages)
           .Include(b => b.Authors)
           .Where(b => b.IsFeatured).ToList(),


                NewBooks = pustokAppDbContext.Books
           .Include(b => b.BookImages)
           .Include(b => b.Authors)
           .Where(b => b.IsNew).ToList(),
                DiscountedBooks = pustokAppDbContext.Books
           .Include(b => b.BookImages)
           .Include(b => b.Authors)
           .Where(b => b.DiscountPercent > 0).ToList()
            };
            return View(homeVm);
        }
    }
}
