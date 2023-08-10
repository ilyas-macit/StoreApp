using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EntityFramework;

public class ProductRepository :RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(StoreAppContext context) : base(context)
    {
    }

    public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

    public Product? GetById(int id, bool trackChanges) => FindByCondition(p => p.Id == id, trackChanges);
    public void CreateProduct(Product product) => Create(product);
    public void UpdateProduct(Product product) => Update(product);
    public void DeleteProduct(Product product) => Delete(product);
}