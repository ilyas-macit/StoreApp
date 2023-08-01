using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages;

public class CartModel : PageModel
{
    private IServiceManager _serviceManager;
    public Cart Cart { get; set; }
    
    public CartModel(IServiceManager serviceManager, Cart cart)
    {
        _serviceManager = serviceManager;
        Cart = cart;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost(int id)
    {
        Product product = _serviceManager.ProductService.GetById(id,false);
        if (product is not null)
        {
            Cart.AddLine(product, 1);
        }
        return Page();
    }

    public void OnPostRemove(int id)
    {
        Product? product = _serviceManager.ProductService.GetById(id, false);
        if (product is not null)
        {
            Cart.RemoveLine(product); 
        }
    }
}