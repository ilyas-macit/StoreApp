using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        builder.HasData(
            new Category() {Id = 1,CategoryName = "Smart Phone" },
            new Category(){Id = 2, CategoryName = "Laptop"},
            new Category(){Id = 3, CategoryName = "Keyboard"}
        );
    }
}