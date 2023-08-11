﻿using Entities.Models;

namespace Repositories.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    IQueryable<Product> GetAllProducts(bool trackChanges);
    IQueryable<Product> GetShowCaseProducts(bool trackChanges);
    Product? GetById(int id, bool trackChanges);
    void CreateProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
}