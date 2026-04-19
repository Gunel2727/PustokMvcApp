using PustokMvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using PustokMvcApp.Models;
using PustokMvcApp.Extensions;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController(PustokAppDbContext _context) : Controller
    {
        // GET: Read all sliders
        public IActionResult Index()
        {
            var sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new slider
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
               
            }
            if (slider.File == null)
            {
                ModelState.AddModelError("File", "Image is required");
                return View(slider);
            }

            var file = slider.File;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("File", "Please select an image file.");
                 return View(slider);
            }
            if(file.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "Image size must be less than 2MB.");
                return View(slider);
            }
           
            Slider newSlider= new Slider
            {
                Title = slider.Title,
                Description = slider.Description,
                ImageUrl = file.SaveFile(@"wwwroot/assets/image/bg-images"),
                ButtonText = slider.ButtonText,
                ButtonUrl = slider.ButtonUrl
            };
            _context.Sliders.Add(newSlider);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public IActionResult Edit(int id)
        {
            var slider = _context.Sliders.Find(id);

            if (slider == null)
                return NotFound();

            return View(slider);
        }

        // POST: Update slider
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Slider slider)
        {
            if(id != slider.Id)
                return BadRequest();
           

            var existSlider = _context.Sliders.Find(id);

            if (existSlider == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(slider);

            // 🔴 Əgər yeni şəkil seçilibsə
            if (slider.File != null)
            {
                var file = slider.File;
                if(!file.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("File", "Please select an image file.");
                    return View(slider);
                }
                if(file.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("File", "Image size must be less than 2MB.");
                    return View(slider);
                }

                if(!string.IsNullOrEmpty(existSlider.ImageUrl))
                {
                    string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/bg-images", existSlider.ImageUrl);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                 


               
                existSlider.ImageUrl = file.SaveFile(@"wwwroot/assets/image/bg-images");
            }

            // 🔴 digər fieldlər
            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            existSlider.ButtonText = slider.ButtonText;
            existSlider.ButtonUrl = slider.ButtonUrl;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Delete slider
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.Find(id);

            if (slider == null)
                return NotFound();
            if(!string.IsNullOrEmpty(slider.ImageUrl))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/bg-images", slider.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
