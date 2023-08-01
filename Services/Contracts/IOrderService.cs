using Entities.Models;

namespace Services.Contracts;

public interface IOrderService
{
    IQueryable<Order> Orders();
    Order GetById(int id);
    void Complete(int id);
    void SaveOrder(Order order);
    public int NumberOfInProgress { get; }
}