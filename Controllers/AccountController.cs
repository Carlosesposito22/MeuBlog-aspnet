using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userVM)
        {
            if (ModelState.IsValid) return View(userVM);

            var user = await _userManager.FindByNameAsync(userVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, userVM.UserPassword, false, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(userVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(userVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login!");
            return View(userVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                User user = new User { UserName = userVM.UserName };
                var result = await _userManager.CreateAsync(user, userVM.UserPassword);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Falha ao fazer o registro");
                }
            }
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
