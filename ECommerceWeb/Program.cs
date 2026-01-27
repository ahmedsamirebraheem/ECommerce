using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DataSeed;
using ECommerce.Presistance.Data.DbContexts;
using ECommerce.Presistance.Repository;
using ECommerce.Service;
using ECommerce.Service.MappingProfiles;
using ECommerce.ServiceAbstraction;
using ECommerceWeb.Extetions;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb
{
    public class Program
    {
        // «· ’ÕÌÕ:  €ÌÌ— void ≈·Ï Task
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1.  ”ÃÌ· «·Œœ„«  «·√”«”Ì…
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // 2. ≈⁄œ«œ Mapster ( √ﬂœ „‰  — Ì» «·”ÿÊ— œÌ)
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(ProductProfile).Assembly);
            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();

            // 3.  ”ÃÌ· Œœ„«  «·„‘—Ê⁄ (Dependency Injection)
            // „·«ÕŸ…:  √ﬂœ √‰ DataInitializer ﬂ·«” Ê·Ì” Interface ›Ì «·”ÿ— «·ﬁ«œ„
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // 4.  ‘€Ì· «·⁄„·Ì«  €Ì— «·„ “«„‰… (Async Operations)
            await app.MigrateDatabaseAsync();
            await app.SeedDataAsync();

            // 5. ≈⁄œ«œ Middleware
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
            app.MapControllers();

            // «” Œœ«„ RunAsync √›÷· ÿ«·„« «·„ÌÀÊœ Task
            await app.RunAsync();
        }
    }
}