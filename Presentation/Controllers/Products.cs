using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/products")]
public class Products : ControllerBase
{
    private readonly IServiceManager _manager;

    public Products(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_manager.ProductService.GetAllProducts(false));
    }

}