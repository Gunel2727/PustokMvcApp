using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokMvcApp.Areas.Manage.ViewModels;
using PustokMvcApp.Models;

namespace PustokMvcApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdminAccountController
        (
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager
        )
        : Controller
    {
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                UserName = "_admin",
                FullName = "Admin Adminov",
                Email = "admin@example.com"
            };
            IdentityResult result = await userManager.CreateAsync(admin, "_Admin123!");
            if (!result.Succeeded)
            {
                return Json(result.Errors);
            }
            await userManager.AddToRoleAsync(admin, "Admin");
            return Content("Admin user created successfully");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(adminLoginVm);
            }
            AppUser admin = await userManager.FindByNameAsync(adminLoginVm.Username);
            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(adminLoginVm);
            }
            bool isPasswordValid = await userManager.CheckPasswordAsync(admin, adminLoginVm.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(adminLoginVm);
            }
           await signInManager.SignInAsync(admin, true);
            return RedirectToAction("index", "dashboard");

        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var user=await userManager.GetUserAsync(HttpContext.User);
            return Json(user);
        }
    }
}
