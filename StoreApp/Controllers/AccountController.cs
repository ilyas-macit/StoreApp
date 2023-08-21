using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // GET
    public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
    {
        return View(new LoginModel()
        {
            Returnurl = ReturnUrl
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.Name);
            if (user is not null)
            {
                //oturum acma 
                await _signInManager.SignOutAsync();
                if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                {
                    return Redirect(model.Returnurl);
                }
            }
            ModelState.AddModelError("Error", "Invalid username or password.");
        }
        return View();
    }

    public async Task<IActionResult> LogOut([FromQuery(Name = "ReturnUrl")] string ReturnUrl ="/")
    {
        await _signInManager.SignOutAsync(); 
        return Redirect(ReturnUrl);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
    {
        var user = new IdentityUser()
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email
        };
        var result = _userManager.CreateAsync(user, registerDto.Password).Result;
        if (result.Succeeded)
        {
            var role = await _userManager
                .AddToRoleAsync(user, "User");
            if (role.Succeeded)
            {
                return RedirectToAction("Login");
            }
        }
        else
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description); 
            }
        }

        return View();
    }

    public IActionResult AccessDenied([FromQuery(Name = "ReturnUrl")] string ReturnUrl)
    {
        return View();
    }

}