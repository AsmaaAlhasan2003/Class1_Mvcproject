using CulturalCenter.Application.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulturalCenter.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<CulturalCenterApplicationUser> _signInManager; 
        private readonly UserManager<CulturalCenterApplicationUser> _userManager; 
        public AccountController(SignInManager<CulturalCenterApplicationUser> signInManager, UserManager<CulturalCenterApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
          
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
            }

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        
        public IActionResult Login()
        {
            ViewData["Title"] = "Log in";
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }
    }
}
