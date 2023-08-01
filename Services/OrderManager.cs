using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class OrderManager : IOrderService
{
    private readonly IRepositoryManager _manager;

    public OrderManager(IRepositoryManager manager)
    {
        _manager = manager;
    }


    public IQueryable<Order> Orders()
    {
        return _manager.Order.Orders();
    }

    public Order GetById(int id)
    {
        return _manager.Order.GetById(id);
    }

    public void Complete(int id)
    {
        _manager.Order.Complete(id);
    }

    public void SaveOrder(Order order)
    {
        _manager.Order.SaveOrder(order);
    }

    public int NumberOfInProgress => _manager.Order.NumberOfInProgress;
}