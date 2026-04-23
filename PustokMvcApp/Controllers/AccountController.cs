using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Models;
using PustokMvcApp.ViewModels.UserVm;

namespace PustokMvcApp.Controllers
{
    public class AccountController
        (
        UserManager<AppUser> userManager,   
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager
        )
        : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

           var user=await userManager.FindByNameAsync(vm.UsernameOrEmail) ?? await userManager.FindByEmailAsync(vm.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or Password is incorrect");
                return View(vm);
            }
            if(await userManager.IsInRoleAsync(user,"Admin"))
            {
                ModelState.AddModelError("", "Admins cannot login here");
                return View(vm);
            }


            var result = await signInManager.PasswordSignInAsync(
                user,
                vm.Password,
                vm.RememberMe,
                true // lockoutOnFailure
            );
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked. Please try again later.");
                return View(vm);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email or Password is incorrect");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user=await userManager.FindByNameAsync(vm.Username);
            if(user != null)
            {
                ModelState.AddModelError("Username", "This username is already taken");
                return View(vm);
            }
            

             user=new AppUser
            {
                FullName = vm.FullName,
                UserName = vm.Username,
                Email = vm.Email
            };

            var result=await userManager.CreateAsync(user,vm.Password);

            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            
            await userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile(string tab="dashboard")
        {
            ViewBag.Tab = tab;
            var user = await userManager.GetUserAsync(User);
           
            var vm = new UserProfileVm
            {
                UserInfo = new UserProfileInfoVm
                {
                    FullName = user.FullName,
                    Username = user.UserName,
                    Email = user.Email
                }
            };

            return View(vm);
        }

        [HttpPost]
       [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile(UserProfileVm vm)
        {
            ViewBag.Tab ="profile";
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

           
            

            // -------------------------
            // BASIC INFO UPDATE
            // -------------------------
            user.FullName = vm.UserInfo.FullName;
            user.UserName = vm.UserInfo.Username;
            user.Email = vm.UserInfo.Email;

            var updateresult = await userManager.UpdateAsync(user);

            if (!updateresult.Succeeded)
            {
                foreach (var error in updateresult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }


            if(!string.IsNullOrEmpty(vm.UserInfo.CurrentPassword) && !string.IsNullOrEmpty(vm.UserInfo.NewPassword))
            {
                var changepasswordresult = await userManager.ChangePasswordAsync(user, vm.UserInfo.CurrentPassword, vm.UserInfo.NewPassword);
                if (!changepasswordresult.Succeeded)
                {
                    foreach (var error in changepasswordresult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(vm);
                }
               
            }
            await signInManager.RefreshSignInAsync(user);
            return RedirectToAction("UserProfile");
        }







        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole ("Admin"));
        //    await roleManager.CreateAsync(new IdentityRole("User"));
        //    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    return Content("Role created");
        //}


    }
}
