using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class CategoryManager : ICategoryService
{
    private readonly IRepositoryManager _manager;

    public CategoryManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public IEnumerable<Category> GetAllCategories(bool trackChanges)
    {
        return _manager.Category.GetAllCategory(trackChanges);
    }
    
    public Category GetById(int id, bool trackChanges)
    {
        var category = _manager.Category.GetById(id, trackChanges);
        if (category is null)
        {
            throw new Exception("Category Not Found");
        }

        return category;
    }
}