using CollegeManagementSystem.Models;
using CollegeManagementSystem.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager)
        {
            userManager = usermanager;
            signInManager = signinmanager;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnURL = null)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                    if (returnURL != null)
                        return LocalRedirect(returnURL);
                    else
                        return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Faild login attempt.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) { 
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    city = model.City,
                    UserName = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Errors = "";
                    foreach (var error in result.Errors)
                        ViewBag.Errors += error.ToString() + "\n";
                    return View(model);
                }
            }
            else
            {
                ViewBag.Errors = "Data invalid";
                return View(model);
            }
        }

    }
}
