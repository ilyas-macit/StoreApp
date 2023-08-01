using Entities.Models;

namespace Repositories.Contracts;

public interface IOrderRepository : IRepositoryBase<Order>
{
     IQueryable<Order> Orders();
     Order GetById(int id);
     void Complete(int id);
     void SaveOrder(Order order);
     public int NumberOfInProgress { get; }
}