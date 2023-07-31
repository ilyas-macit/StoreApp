using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components;

public class CategoriesSummaryViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;

    public CategoriesSummaryViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }

    public string Invoke(int id)
    {
        var products = _manager.ProductService.GetAllProducts(false).Where(p => p.CategoryId == id).ToList();
        return products.Count().ToString();
    }
}