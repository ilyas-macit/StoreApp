using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;

namespace Repositories;
public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options): base(options)
    {
         
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}