using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
        service.AddDbContext<StoreAppContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
                b => b.MigrationsAssembly("StoreApp")
            );
            option.EnableSensitiveDataLogging(true);
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;

            })
            .AddEntityFrameworkStores<StoreAppContext>();
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
        service.AddScoped<IAuthService, AuthManager>();
    }

    public static void ConfigureApplicationCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(option =>
        {
            option.LoginPath = new PathString("/Account/Login");
            option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            option.AccessDeniedPath = new PathString("/Account/AccessDenied");
        });
    }
    public static void ConfigureRouting(this IServiceCollection service)
    {
        service.AddRouting(options =>
        {
            options.LowercaseUrls = true; // routh'daki karakterleri küçültür
            options.AppendTrailingSlash = true; // sona slash ekler 
        });
    }
}