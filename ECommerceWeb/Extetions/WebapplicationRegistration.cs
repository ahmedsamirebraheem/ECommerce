using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DbContexts;
using ECommerce.Presistance.IdentityData.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceWeb.Extetions;

public static class WebapplicationRegistration
{
    public async static Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
    {
       await using var scope = app.Services.CreateAsyncScope();
        var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
        var pendingMigration = await dbContextService.Database.GetPendingMigrationsAsync();
        if (pendingMigration.Any())
        {
           await dbContextService.Database.MigrateAsync();
        }
        return app;
    }

    public async static Task<WebApplication> MigrateIdentityDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContextService = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
        var pendingMigration = await dbContextService.Database.GetPendingMigrationsAsync();
        if (pendingMigration.Any())
        {
            await dbContextService.Database.MigrateAsync();
        }
        return app;
    }

    public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
         var dataInitializerService = scope.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Default");
        await dataInitializerService.InitializeAsync();
        return app;
    }

    public static async Task<WebApplication> SeedIdentityAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dataInitializerService = scope.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Identity");
        await dataInitializerService.InitializeAsync();
        return app;
    }
}
