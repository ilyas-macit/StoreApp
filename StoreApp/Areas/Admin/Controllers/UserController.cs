using System.Collections;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IServiceManager _serviceManager;

    public UserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _serviceManager.AuthService.GetAllUsers();
        return View(model);
    }

    public IActionResult Create()
    {
        return View(new UserDtoForCreation()
        {
            Roles = new HashSet<string>(
                _serviceManager
                    .AuthService
                    .Roles
                    .Select(r => r.Name)!
            )
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
    {
        var result = _serviceManager.AuthService.CreateUser(userDto);
        if (result.Result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        return View();
    }


    public async Task<IActionResult> Update([FromRoute(Name = "id")] string id)
    {
        var user = await _serviceManager.AuthService.GetOneUserForUpdate(id);
        return View(user);
    }


    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
    {
        await _serviceManager.AuthService.Update(userDto);
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> ResetPassword([FromRoute(Name = "id")] string id)
    {
        return View(new ResetPasswordDto()
        {
            UserName = id
        });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
    {
        var result = await _serviceManager.AuthService.ResetPassword(model);
        Console.Write("a");
        return result.Succeeded
            ? RedirectToAction("Index")
            : View();
    }

    public async Task<IActionResult> DeleteOneUser([FromRoute(Name = "id")] string id)
    {
        var result = await _serviceManager.AuthService.DeleteOneUser(id);
        return RedirectToAction("Index");
    }
}