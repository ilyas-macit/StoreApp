using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts(bool trackChanges);
    IEnumerable<Product> GetShowCaseProducts(bool trackChanges);

    Product? GetById(int id, bool trackChanges);
    void CreateProduct(ProductDtoForInsertions product);
    void UpdateProduct(ProductDtoForUpdate productDto);
    void DeleteProduct(Product product);

    ProductDtoForUpdate GetProductForUpdateById(int id, bool b);
}