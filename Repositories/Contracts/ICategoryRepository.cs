using Entities.Models;

namespace Repositories.Contracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    IQueryable<Category> GetAllCategory(bool trackChanges);
    Category? GetById(int id, bool trackChanges);
    
}