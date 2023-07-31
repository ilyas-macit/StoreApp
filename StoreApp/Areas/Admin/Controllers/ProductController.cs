using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    
    private readonly IServiceManager _manager;
    private readonly IMapper _mapper;
    public ProductController(IServiceManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var products = _manager.ProductService.GetAllProducts(false);
        return View(products);
    }

    public IActionResult Create()
    {
        
        // ViewBag.Categories = _manager.CategoryService.GetAllCategories(false);
        ViewBag.Categories = new SelectList(
            _manager.CategoryService.GetAllCategories(false),
            "Id",
            "CategoryName",
            "1"
            );
        return View();
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] ProductDtoForInsertions productDto, IFormFile file)
    {
        
        if (ModelState.IsValid)
        {
            
            string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Images",file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
               await file.CopyToAsync(stream); 
            }

            productDto.ImageUrl = string.Concat("/Images/", file.FileName);
            _manager.ProductService.CreateProduct(productDto);
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Update([FromRoute] int id)
    {
        var product = _manager.ProductService.GetProductForUpdateById(id ,false);
        ViewBag.Categories = new SelectList(_manager.CategoryService.GetAllCategories(false),
            "Id",
            "CategoryName",
            "1");
        return View(product);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProductDtoForUpdate productDto, IFormFile file)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", file.FileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        productDto.ImageUrl = String.Concat("/Images/", file.FileName);
        _manager.ProductService.UpdateProduct(productDto);
        return RedirectToAction("Index");
    }

    public IActionResult Delete([FromRoute] int id)
    {
        var product = _manager.ProductService.GetById(id, false);
        _manager.ProductService.DeleteProduct(product);
        return RedirectToAction("Index");
    }
    
}