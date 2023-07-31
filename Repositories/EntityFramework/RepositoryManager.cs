using Repositories.Contracts;

namespace Repositories.EntityFramework;

public class RepositoryManager : IRepositoryManager
{
    private readonly IProductRepository _productRepository;
    private readonly RepositoryContext _context;
    private ICategoryRepository _categoryRepository;

    public RepositoryManager(IProductRepository productRepository, RepositoryContext context, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _context = context;
    }

    public IProductRepository Product => _productRepository;
    public ICategoryRepository Category => _categoryRepository;

    public void Save()
    {
        _context.SaveChanges();
    }
    
}