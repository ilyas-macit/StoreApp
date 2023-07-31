using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components;

public class CategoriesMenuViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;

    public CategoriesMenuViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _manager.CategoryService.GetAllCategories(false).ToList();
        return View(categories);
    }
}