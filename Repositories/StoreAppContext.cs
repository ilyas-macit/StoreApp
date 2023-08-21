using System.Reflection;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;

namespace Repositories;
public class StoreAppContext : IdentityDbContext<IdentityUser>
{
    public StoreAppContext(DbContextOptions options): base(options)
    {
         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        /*
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Category).Assembly);
        */
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    
}