using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.Presistance.Data.DataSeed;
using ECommerce.Presistance.Data.DbContexts;
using ECommerce.Presistance.IdentityData.DataSeed;
using ECommerce.Presistance.IdentityData.DbContext;
using ECommerce.Presistance.Repository;
using ECommerce.Service;
using ECommerce.Service.MappingProfiles;
using ECommerce.ServiceAbstraction;
using ECommerceWeb.CustomMiddleWare;
using ECommerceWeb.Extetions;
using ECommerceWeb.Factory;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
namespace ECommerceWeb;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1.  ”ÃÌ· «·Œœ„«  «·√”«”Ì… (Controllers & Swagger)
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 2. ﬁ«⁄œ… «·»Ì«‰« 
        builder.Services.AddDbContext<StoreDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // 3. ≈⁄œ«œ Mapster («·ÿ—Ìﬁ… «·ÌœÊÌ… «·„÷„Ê‰… · Ã‰» √Œÿ«¡ «·„ﬂ »« )
        var config = new TypeAdapterConfig(); // Instance ÃœÌœ »œ· GlobalSettings
        config.Scan(typeof(ProductProfile).Assembly);

        builder.Services.AddSingleton(config);
        builder.Services.AddScoped<IMapper, ServiceMapper>();

        // 4.  ”ÃÌ· Œœ„«  «·‹ Dependency Injection (›ﬂ «·ﬂÊ„‰  Â‰«)
        builder.Services.AddKeyedScoped<IDataInitializer, DataInitializer>("Default");
        builder.Services.AddKeyedScoped<IDataInitializer, IdentityDataInitializer>("Identity");

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddScoped<IBasketService, BasketService>();
        builder.Services.AddScoped<ICacheRepository, CacheRepository>();
        builder.Services.AddScoped<ICacheService, CacheService>();
        builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
        {
            return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
        });

        //  ”ÃÌ· «·‹ Resolver «·ÃœÌœ ﬂŒœ„…
        builder.Services.AddScoped<IPictureUrlResolver, PictureUrlResolver>();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
        });

        builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
        });

        builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

        var app = builder.Build();

        

        app.UseMiddleware<ExceptionHandlerMiddleWare>();

        // 5.  ‘€Ì· «·„«ÌÃ—Ì‘‰ Ê«·‹ Seed
        await app.MigrateDatabaseAsync();
        await app.MigrateIdentityDatabaseAsync();
        await app.SeedDataAsync();
        await app.SeedIdentityAsync();

        // 6. «·‹ Middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        // „Â„ Ãœ« ⁄‘«‰ «·’Ê—  › Õ
        app.UseStaticFiles();

        app.MapControllers();

        await app.RunAsync();
    }
}