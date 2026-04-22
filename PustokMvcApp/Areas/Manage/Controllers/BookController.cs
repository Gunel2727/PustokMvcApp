using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
using PustokMvcApp.Extensions;
using PustokMvcApp.Models;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController(PustokAppDbContext pustokAppDbContext) : Controller
    {
        public IActionResult Index()
        {
            var books = pustokAppDbContext.Books.Include(b => b.Author).ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = pustokAppDbContext.Authors.ToList();
            ViewBag.Tags = pustokAppDbContext.Tags.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {

            ModelState.Remove("MainImageUrl");
            ModelState.Remove("HoverImageUrl");
            ModelState.Remove("Author");
            ViewBag.Authors = pustokAppDbContext.Authors.ToList();
            ViewBag.Tags = pustokAppDbContext.Tags.ToList();

            if (!ModelState.IsValid)
            {

                return View(book);
            }
            if (!pustokAppDbContext.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found");
                return View(book);
            }
            if (book.TagIds != null && book.TagIds.Any())
            {
                var validTagIds = pustokAppDbContext.Tags.Select(t => t.Id).ToList();
                foreach (var tagId in book.TagIds)
                {
                    if (!validTagIds.Contains(tagId))
                    {
                        ModelState.AddModelError("TagIds", $"Tag with ID {tagId} not found");
                        return View(book);
                    }
                    book.BookTag.Add(new BookTag
                    {
                        TagId = tagId
                    });
                }
            }

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/products");

            // -------------------------
            // MAIN IMAGE
            // -------------------------
            if (book.MainPhoto == null)
            {
                ModelState.AddModelError("MainPhoto", "Main photo is required");
                return View(book);

            }
            book.MainImageUrl = book.MainPhoto.SaveFile(imagePath);

            // -------------------------
            // HOVER IMAGE
            // -------------------------
            if (book.HoverPhoto == null)
            {
                ModelState.AddModelError("HoverPhoto", "Hover photo is required");
                return View(book);

            }


            book.HoverImageUrl = book.HoverPhoto.SaveFile(imagePath);


            if (book.Files != null && book.Files.Any())
            {
                foreach (var file in book.Files)
                {
                    if (file != null)
                    {
                        book.BookImage.Add(new BookImage
                        {
                            ImageUrl = file.SaveFile(imagePath)
                        });
                    }
                }
            }

            // -------------------------
            // SAVE BOOK
            // -------------------------

            pustokAppDbContext.Books.Add(book);
            pustokAppDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = pustokAppDbContext.Books
                .Include(b => b.BookTag)
                .FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
            ViewBag.Authors = pustokAppDbContext.Authors.ToList();
            ViewBag.Tags = pustokAppDbContext.Tags.ToList();
            book.TagIds = book.BookTag.Select(bt => bt.TagId).ToList();
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Book book)
        {
            ModelState.Remove("MainImageUrl");
            ModelState.Remove("HoverImageUrl");
            ModelState.Remove("Author");
            if (id!=book.Id)
            {
                return BadRequest();
            }
            ViewBag.Authors = pustokAppDbContext.Authors.ToList();
            ViewBag.Tags = pustokAppDbContext.Tags.ToList();

            var existBook = pustokAppDbContext.Books
                .Include(b => b.BookTag)
                .Include(b => b.BookImage)
                .FirstOrDefault(b => b.Id == book.Id);

            if (existBook == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            if(!pustokAppDbContext.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found");
                return View(book);
            }

           
             if (book.TagIds != null && book.TagIds.Any())
            {
                var validTagIds = pustokAppDbContext.Tags.Select(t => t.Id).ToList();
                var tagToRemove = existBook.BookTag.Where(bt => !book.TagIds.Contains(bt.TagId)).ToList();
               foreach (var item in tagToRemove)
                {
                    existBook.BookTag.Remove(item);
                }
                foreach (var tagId in book.TagIds)
                {
                    if (!validTagIds.Contains(tagId))
                    {
                        ModelState.AddModelError("TagIds", $"Tag with ID {tagId} not found");
                        return View(book);
                    }
                    if (!existBook.BookTag.Any(bt => bt.TagId == tagId))
                    {
                        existBook.BookTag.Add(new BookTag
                        {
                            BookId = id,
                            TagId = tagId
                        });
                    }
                }

            }
            else
            {
                existBook.BookTag.Clear();
            }
                // -------------------------
                // BASIC FIELDS
                // -------------------------
            existBook.Name = book.Name;
            existBook.Description = book.Description;
            existBook.Price = book.Price;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.Code = book.Code;
            existBook.AuthorId = book.AuthorId;
            existBook.InStock = book.InStock;
            existBook.IsFeatured = book.IsFeatured;
            existBook.IsNew = book.IsNew;

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/assets/image/products");

            // -------------------------
            // MAIN IMAGE (optional replace)
            // -------------------------
            if (book.MainPhoto != null)
            {
                if(!string.IsNullOrEmpty(existBook.MainImageUrl))
                {
                    string existMainImagePath = Path.Combine(imagePath, existBook.MainImageUrl);
                    if (System.IO.File.Exists(existMainImagePath))
                    {
                        System.IO.File.Delete(existMainImagePath);
                    }
                    existBook.MainImageUrl = book.MainPhoto.SaveFile(imagePath);
                }
            }

            // -------------------------
            // HOVER IMAGE (optional replace)
            // -------------------------
            if (book.HoverPhoto != null)
            {
                if (!string.IsNullOrEmpty(existBook.HoverImageUrl))
                {
                    string existHoverImagePath = Path.Combine(imagePath, existBook.HoverImageUrl);
                    if (System.IO.File.Exists(existHoverImagePath))
                    {
                        System.IO.File.Delete(existHoverImagePath);
                    }
                    existBook.HoverImageUrl = book.HoverPhoto.SaveFile(imagePath);
                }
            }

            // -------------------------
            // NEW IMAGES (optional add)
            // -------------------------
            if (book.Files != null && book.Files.Any())
            {
                foreach (var file in book.Files)
                {
                    existBook.BookImage.Add(new BookImage
                    {
                        ImageUrl = file.SaveFile(imagePath)
                    });
                }
            }

            // -------------------------
            // SAVE
            // -------------------------
            pustokAppDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
