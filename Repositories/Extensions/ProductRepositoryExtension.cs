using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Extensions;

public static class ProductRepositoryExtension
{
    public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? categoryId)
    {
        return categoryId is null
            ? products 
                .Include(p => p.Category)
            : products
                .Include(p => p.Category)
                .Where(p => p.CategoryId.Equals(categoryId));
    }

    public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, string? seacryTerm)
    {
        return string.IsNullOrWhiteSpace(seacryTerm)
            ? products
            : products.Where(p => p.ProductName.ToLower().Contains(seacryTerm));
    }

    public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int minPrice, int maxPrice, bool isValidPrice)
    {
        return isValidPrice
            ? products.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            : products;
    }
}