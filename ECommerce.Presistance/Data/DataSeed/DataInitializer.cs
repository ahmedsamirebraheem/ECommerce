using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Presistance.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace ECommerce.Presistance.Data.DataSeed;

public class DataInitializer(StoreDbContext dbContext) : IDataInitializer
{
    // الحل لرسالة CA1869: تعريف الإعدادات مرة واحدة وإعادة استخدامها
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task InitializeAsync()
    {
        try
        {
            // 1. Seed Brands
            if (!await dbContext.ProductBrands.AnyAsync())
            {
                await SeedDataFromJson<ProductBrand, int>("brands.json", dbContext.ProductBrands);
                await dbContext.SaveChangesAsync(); // حفظ فوراً عشان الـ IDs تتولد
            }

            // 2. Seed Types
            if (!await dbContext.ProductTypes.AnyAsync())
            {
                await SeedDataFromJson<ProductType, int>("types.json", dbContext.ProductTypes);
                await dbContext.SaveChangesAsync();
            }

            // 3. Seed Products
            if (!await dbContext.Products.AnyAsync())
            {
                await SeedDataFromJson<Product, int>("products.json", dbContext.Products);
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.DeliveryMethods.AnyAsync())
            {
                await SeedDataFromJson<DeliveryMethod, int>("delivery.json", dbContext.DeliveryMethods);
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            // اطبع الـ InnerException لأنه هو اللي بيقول السبب الحقيقي لو فيه مشكلة في الـ SQL
            var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            Console.WriteLine($"Error during data seeding: {message}");
        }
    }
    private static async Task SeedDataFromJson<T, Tkey>(string fileName, DbSet<T> dbset) where T : BaseEntity<Tkey>
    {
        // 1. هنجيب المسار اللي الكود شغال فيه حالياً (bin folder)
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        // 2. هنبني المسار بناءً على هيكل المشروع عندك
        // لو الـ JSONFiles موجودة جوه الـ Persistence، تأكد إنها بتتعمل لها Copy to Output
        var filePath = Path.Combine(assemblyPath!, "Data", "DataSeed", "JSONFiles", fileName);

        // لو لسه مش عارف تظبط المسار، استخدم الحل "القوي" ده للتطوير فقط:
        // var filePath = @"C:\Users\Samir\source\repos\ECommerce\ECommerce.Presistance\Data\DataSeed\JSONFiles\" + fileName;

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"[WARNING] File {fileName} NOT FOUND at: {filePath}");
            return;
        }

        try
        {
            using var dataStream = File.OpenRead(filePath);
            var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, _jsonOptions);

            if (data is not null && data.Count != 0)
            {
                await dbset.AddRangeAsync(data);
                Console.WriteLine($"[SUCCESS] Seeded {data.Count} items from {fileName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] reading json file {fileName}: {ex.Message}");
        }
    }
}