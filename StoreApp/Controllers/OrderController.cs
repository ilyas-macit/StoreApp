using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace StoreApp.Controllers;

public class OrderController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly Cart _cart;

    public OrderController(IServiceManager serviceManager, Cart cart)
    {
        _serviceManager = serviceManager;
        _cart = cart;
    }

    public ViewResult Checkout() => View(new Order());

    [HttpPost]
    public IActionResult Checkout([FromForm] Order order)
    {
        if (_cart.Lines.Count == 0)
        {
            ModelState.AddModelError("", "Sorry, your cart is empty.");
        }

        order.Lines = _cart.Lines.ToArray();
        order.OrderedDate = DateTime.Now;
        _serviceManager.OrderService.SaveOrder(order);
        _cart.Clear();
        return RedirectToPage("/Complete", new { OrderId = order.Id });
    }
}