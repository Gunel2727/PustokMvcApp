using Microsoft.AspNetCore.Mvc;
using PustokMvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using PustokMvcApp.Models;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController(PustokAppDbContext _context) : Controller
    {
        // GET: Read all authors
        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        // POST: Create new author
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FullName")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Update author
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FullName")] Author author)
        {
            if (id != author.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingAuthor = _context.Authors.Find(id);
                if (existingAuthor != null)
                {
                    existingAuthor.FullName = author.FullName;
                    _context.Update(existingAuthor);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Delete author
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
