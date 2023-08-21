using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class RoleController : Controller
{
    private readonly IServiceManager _serviceManager;

    public RoleController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }


    public IActionResult Index()
    {
        var roles = _serviceManager.AuthService.Roles;
        return View(roles);
    }
}