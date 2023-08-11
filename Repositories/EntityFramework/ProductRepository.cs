using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories.EntityFramework;

public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(StoreAppContext context) : base(context)
    {
    }

    public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

    public IQueryable<Product> GetShowCaseProducts(bool trackChanges) => FindAll(trackChanges)
        .Where(p => p.ShowCase.Equals(true));

    public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters parameters)
    {
        return _context
            .Products
            .FilteredByCategoryId(parameters.CategoryId)
            .FilteredBySearchTerm(parameters.SearchTerm)
            .FilteredByPrice(parameters.MinPrice, parameters.MaxPrice, parameters.IsValidPrice);
    }


    public Product? GetById(int id, bool trackChanges) => FindByCondition(p => p.Id == id, trackChanges);
    public void CreateProduct(Product product) => Create(product);
    public void UpdateProduct(Product product) => Update(product);
    public void DeleteProduct(Product product) => Delete(product);
}