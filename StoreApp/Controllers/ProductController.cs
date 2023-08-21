using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Models;


namespace StoreApp.Controllers;

public class ProductController : Controller
{
    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index(ProductRequestParameters parameters)
    {
        var products = _manager.ProductService.GetAllProductsWithDetails(parameters).ToList();
        var pagination = new Pagination()
        {
            CurrentPage = parameters.PageNumber,
            ItemsPerPage = parameters.PageSize,
            TotalItems = _manager.ProductService.GetAllProducts(false).ToList().Count
        };
            
        return View(new ProductListViewModel()
        {
            Products = products,
            Pagination = pagination
        });
    }
    
    
    
    public IActionResult Get([FromRoute(Name = "id")] int id)
    {
        var product = _manager.ProductService.GetById(id,false);
        return View(product);
    }
}