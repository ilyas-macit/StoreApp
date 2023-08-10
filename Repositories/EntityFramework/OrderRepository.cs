using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EntityFramework;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(StoreAppContext context) : base(context)
    {
    }

    public IQueryable<Order> Orders() => _context.Orders
        .Include(o => o.Lines)
        .ThenInclude(cl => cl.Product)
        .OrderBy(o => o.Shipped)
        .ThenByDescending(o => o.Id);

    public Order GetById(int id) => FindByCondition(o => o.Id.Equals(id),false);
    
    public void Complete(int id)
    {
        Order order = FindByCondition(o => o.Id.Equals(id),true);
        if (order is null)
        {
            throw new Exception("Order could not found!");
        }

        order.Shipped = true;
        
    }

    public void SaveOrder(Order order)
    {
        _context.AttachRange(order.Lines.Select(l => l.Product));
        if (order.Id == 0)
        {
            _context.Orders.Add(order);
        }
    }

    public int NumberOfInProgress => _context.Orders.Count(o => o.Shipped.Equals(false));
}