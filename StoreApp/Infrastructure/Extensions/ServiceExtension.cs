using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.EntityFramework;
using Services;
using Services.Contracts;

namespace StoreApp.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureDbContext(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<StoreAppContext>(option
            => option.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
                b => b.MigrationsAssembly("StoreApp")
            )
        );
    }

    public static void ConfigureSession(this IServiceCollection service)
    {
        service.AddDistributedMemoryCache();
        service.AddSession(option =>
        {
            option.Cookie.Name = "StoreApp.Session";
            option.IdleTimeout = TimeSpan.FromMinutes(10);
        });
    }

    public static void ConfigureRepositoryRegistration(this IServiceCollection service)
    {
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureServiceRegistration(this IServiceCollection service)
    {
        service.AddScoped<IProductService, ProductManager>();
        service.AddScoped<ICategoryService, CategoryManager>();
        service.AddScoped<IServiceManager, ServiceManager>();
        service.AddScoped<IOrderService, OrderManager>();
    }

    public static void ConfigureRouting(this IServiceCollection service)
    {
        service.AddRouting(options =>
        {
            options.LowercaseUrls = true;// routh'daki karakterleri küçültür
            options.AppendTrailingSlash = true; // sona slash ekler 
        });
        
    } 
}