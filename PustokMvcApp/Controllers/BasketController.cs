using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PustokMvcApp.Data;
using PustokMvcApp.Models;
using PustokMvcApp.Services;
using PustokMvcApp.Settings;
using PustokMvcApp.ViewModels;

namespace PustokMvcApp.Controllers
{
    public class BasketController(PustokAppDbContext pustokAppDbContext) : Controller
    {
        public IActionResult AddBasket(int Id)
        {
            var book= pustokAppDbContext.Books.Find(Id);
            if (book == null) {
                return NotFound();
            }
            List<BasketItemVm> basketItems;
            var basketStr = Request.Cookies["Basket"];
            if (string.IsNullOrEmpty(basketStr))
            {
                basketItems = new List<BasketItemVm>();
            }
            else
            {
                basketItems = System.Text.Json.JsonSerializer.Deserialize<List<BasketItemVm>>(basketStr);
            }
            var existBasketItem = basketItems.FirstOrDefault(x => x.BookId == Id);
            if (existBasketItem == null)
            {
                basketItems.Add(new BasketItemVm
                {
                    BookId = book.Id,
                    Name = book.Name,
                    Price = book.DiscountPercent > 0 ? book.Price - (book.Price * book.DiscountPercent / 100) : book.Price,
                    Count = 1,
                    MainImageUrl = book.MainImageUrl
                });
            }
            else
            {
                existBasketItem.Count++;
            }
            if (User.Identity.IsAuthenticated)
            {
                var user = pustokAppDbContext.Users
                    .Include(x => x.BasketItems)
                    .FirstOrDefault(x => x.UserName == User.Identity.Name);
                var existBasketItemDb = user.BasketItems.FirstOrDefault(x => x.BookId == Id);
                if (existBasketItemDb == null)
                {
                    pustokAppDbContext.BasketItems.Add(new BasketItem
                    {
                        BookId = book.Id,
                        Count = 1,
                        AppUserId = user.Id
                    });
                }
                else
                {
                    existBasketItemDb.Count++;
                }
                pustokAppDbContext.SaveChanges();
            }
            Response.Cookies.Append("Basket", System.Text.Json.JsonSerializer.Serialize(basketItems));
            return PartialView("_BasketPartial",basketItems);
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
