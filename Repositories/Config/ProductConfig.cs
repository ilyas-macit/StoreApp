using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.HasData(
            new Product() {Id = 1,ProductName = "Logitech pro x", Price = 2500, CategoryId = 3 ,ImageUrl = "1_org_zoom.webp", ShowCase = true},
            new Product(){Id = 2,ProductName = "IPhone 13" , Price = 30000 , CategoryId =1 ,ImageUrl = "1_org_zoom (1).webp", ShowCase = true},
            new Product() {Id = 3,ProductName = "Acer nitro 5", Price = 40000, CategoryId = 2, ImageUrl = "pro-x-keyboard-gallery-1.png", ShowCase = true}
        );
    }
}