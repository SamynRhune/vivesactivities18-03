using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VivesActivities.Ui.Mvc.Models;

namespace VivesActivities.Ui.Mvc.Controllers
{
    public class IdentityController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> SignIn(string? returnUrl)
        {
            await signInManager.SignOutAsync();

            ViewBag.ReturnUrl = returnUrl ?? "/";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model, string? returnUrl)
        {
            returnUrl ??= "/";

            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl ?? "/";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string? returnUrl)
        {
            returnUrl ??= "/";

            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var user = new IdentityUser(model.Username)
            {
                Email = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ViewBag.ReturUrl = returnUrl;
                return View(model);
            }

            await signInManager.SignInAsync(user, false);

            return LocalRedirect(returnUrl);
        }
    }
}
