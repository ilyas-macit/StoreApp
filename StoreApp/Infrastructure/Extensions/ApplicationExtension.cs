using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions;

public static class ApplicationExtension
{
    public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
    {
        var context = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<StoreAppContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            //dotnet ef database update
            context.Database.Migrate();
        }
    }

    public static void ConfigureLocalization(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            options.AddSupportedCultures("tr-TR")
                .SetDefaultCulture("tr-TR");
        });
    }


    public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
    {
        const string adminUser = "Admin";
        const string adminPassword = "admin+123";

        //UserManager
        UserManager<IdentityUser> userManager = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();

        //RoleManager
        RoleManager<IdentityRole> roleManager = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        IdentityUser user = await userManager.FindByNameAsync(adminUser);
        if (user is null)
        {
            user = new IdentityUser()
            {
                Email = "ilyasmacit@gmail.com",
                PhoneNumber = "5554443322",
                UserName = adminUser,
                EmailConfirmed = true
                //more
            };
            var result = await userManager.CreateAsync(user, adminPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Admin user could not created");
            }

            var roleResult = await userManager.AddToRolesAsync(user,
                roleManager
                    .Roles
                    .Select(r => r.Name)
                    .ToList()
            );

            if (!roleResult.Succeeded)
            {
                throw new Exception("System have problem with role definition for Admin");
            }
            
        }
    }
}