using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
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
        
       
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
           if(!ModelState.IsValid)
                return View(author);
           if(_context.Authors.Any(a=>a.FullName.ToLower() == author.FullName.ToLower()))
            {
                ModelState.AddModelError("FullName", "This author already exists");
                return View(author);
            }
            Author newAuthor= new Author
            {
               Id= author.Id,
               FullName = author.FullName
            };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Update author
       
        
        public IActionResult Edit(int id)
        {
            var author = _context.Authors.Find(id);
            if (author == null)
            {
                return NotFound();

            }
            return View(author);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
                return View(author);
            var existAuthor = _context.Authors.Find(author.Id);
            if (existAuthor == null)
            {
                return NotFound();
            }
            if(_context.Authors.Any(a => a.FullName.ToLower() == author.FullName.ToLower() && a.Id != author.Id))
            {
                ModelState.AddModelError("FullName", "This author already exists");
                return View(author);
            }
            existAuthor.FullName = author.FullName;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Delete author
        [HttpGet]
       
        public IActionResult Delete(int id)
        {
            var author = _context.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
               
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        
        public IActionResult DetailsModal(int id)
        {
            var author = _context.Authors.Find(id);


            if (author == null)
                return NotFound();

            return PartialView("_AuthorDetailsModal", author);
        }
    }
}
