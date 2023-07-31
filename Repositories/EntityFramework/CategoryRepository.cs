using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EntityFramework;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext context) : base(context)
    {
        
    }

    public IQueryable<Category> GetAllCategory(bool trackChanges) => FindAll(trackChanges);

    public Category? GetById(int id, bool trackChanges) => FindByCondition(c => c.Id == id, trackChanges);
}