using PustokMvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using PustokMvcApp.Models;

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

        // POST: Create new slider
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,ImageUrl,ButtonText,ButtonUrl")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                _context.Sliders.Add(slider);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Update slider
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description,ImageUrl,ButtonText,ButtonUrl")] Slider slider)
        {
            if (id != slider.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingSlider = _context.Sliders.Find(id);
                if (existingSlider != null)
                {
                    existingSlider.Title = slider.Title;
                    existingSlider.Description = slider.Description;
                    existingSlider.ImageUrl = slider.ImageUrl;
                    existingSlider.ButtonText = slider.ButtonText;
                    existingSlider.ButtonUrl = slider.ButtonUrl;
                    _context.Update(existingSlider);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Delete slider
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
