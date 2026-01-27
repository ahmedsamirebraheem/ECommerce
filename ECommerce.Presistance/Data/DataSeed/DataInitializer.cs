using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Presistance.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
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
        // استخدام Path.Combine أفضل للمسارات
        var filePath = Path.Combine("..", "ECommerce.Presistance", "Data", "DataSeed", "JSONFiles", fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File {fileName} does not exist at {filePath}");

        try
        {
            using var dataStream = File.OpenRead(filePath);

            // استخدام الـ _jsonOptions الثابتة هنا
            var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, _jsonOptions);

            if (data is not null && data.Count != 0)
            {
                await dbset.AddRangeAsync(data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while reading json file {fileName}: {ex.Message}");
        }
    }
}