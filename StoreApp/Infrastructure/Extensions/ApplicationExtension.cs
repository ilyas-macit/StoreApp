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
}