using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcWeb.Models;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Email,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                RegistrationTime = model.RegistrationTime,
                LastLoginTime = model.LastLoginTime,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "UserManagement");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        try
        {
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (signInResult.Succeeded)
                {
                    TempData["Message"] = "Logged In";
                    return RedirectToAction("Index", "UserManagement");
                }
            }
        }
        catch (Exception)
        {
            TempData["Message"] = "User Login failed";
            return RedirectToAction(nameof(Index));
        }

        TempData["Message"] = "User doesn't Exist";
        return RedirectToAction(nameof(Index));
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        return RedirectToAction("Index", "Home");
    }
}
