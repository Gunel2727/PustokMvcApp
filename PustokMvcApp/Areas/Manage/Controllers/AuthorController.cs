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
        public IActionResult Create(Author author)
        {
           if(!ModelState.IsValid)
                return View(author);
           Author newAuthor= new Author
            {
               Id= author.Id,
               FullName = author.FullName
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Update author
        [HttpPost]
        
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
