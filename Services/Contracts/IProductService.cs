using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts(bool trackChanges);
    IEnumerable<Product> GetLatestProducts(int n, bool trackChanges);
    IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters parameters);
    IEnumerable<Product> GetShowCaseProducts(bool trackChanges);

    Product? GetById(int id, bool trackChanges);
    void CreateProduct(ProductDtoForInsertions product);
    void UpdateProduct(ProductDtoForUpdate productDto);
    void DeleteProduct(Product product);

    ProductDtoForUpdate GetProductForUpdateById(int id, bool b);
}