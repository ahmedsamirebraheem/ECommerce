using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

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
    public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
         var dataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
        await dataInitializerService.InitializeAsync();
        return app;
    }
}
