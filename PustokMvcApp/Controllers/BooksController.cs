using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
using PustokMvcApp.ViewModels;

namespace PustokMvcApp.Controllers
{
    public class BooksController(PustokAppDbContext pustokAppDbContext) : Controller
    {
        public IActionResult Details(int Id)
        {
            var book=pustokAppDbContext.Books
                .Include(b=>b.Author)
                .Include(b=>b.BookImage)
                .Include(b=>b.BookTag)
                .ThenInclude(bt=>bt.Tag)
                .FirstOrDefault(b=>b.Id==Id);

            BookVm bookVm = new BookVm
            {
               Book=book,
                RelatedBooks=pustokAppDbContext.Books
                .Include(b=>b.Author)
                .Include(b=>b.BookImage)
                .Where(b=>b.AuthorId==book.AuthorId && b.Id!=book.Id)
                .Take(4)
                .ToList()
            };
            return View(bookVm);
        }
    }
}
