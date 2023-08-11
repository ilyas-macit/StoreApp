using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ProductManager : IProductService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;
    public ProductManager(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public IEnumerable<Product> GetAllProducts(bool trackChanges)
    {
        return _manager.Product.GetAllProducts(trackChanges);
    }

    public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters parameters)
    {
        return _manager.Product.GetAllProductsWithDetails(parameters);
    }

    public IEnumerable<Product> GetShowCaseProducts(bool trackChanges)
    {
        return _manager.Product.GetShowCaseProducts(trackChanges);
    }

    public Product? GetById(int id, bool trackChanges)
    {
        var product = _manager.Product.GetById(id, trackChanges);
        if (product is null)
        {
            throw new Exception("Product Not Found");
        }

        return product;
    }

    public void CreateProduct(ProductDtoForInsertions productDtoForInsertions)
    {
        if (productDtoForInsertions is null)
            throw new Exception("Product can not be null!");
        Product product = _mapper.Map<Product>(productDtoForInsertions);
        _manager.Product.CreateProduct(product);
        _manager.Save();
    }

    public void UpdateProduct(ProductDtoForUpdate productDto)
    {
        var product = _mapper.Map<Product>(productDto);
       _manager.Product.UpdateProduct(product); 
       _manager.Save();
    }

    public void DeleteProduct(Product product)
    {
        _manager.Product.DeleteProduct(product);
        _manager.Save();
    }

    public ProductDtoForUpdate GetProductForUpdateById(int id, bool trackChanges)
    {
        var product = _manager.Product.GetById(id, trackChanges);
        return _mapper.Map<ProductDtoForUpdate>(product);
    }
}