using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder : IDataSeeder
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly ILogger<CatalogDataSeeder> logger;

        public CatalogDataSeeder(StoreDbContext storeDbContext, ILogger<CatalogDataSeeder> logger)
        {
            this._storeDbContext = storeDbContext;
            this.logger = logger;
        }

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await _storeDbContext.Database.GetAppliedMigrationsAsync(ct);
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    await _storeDbContext.Database.MigrateAsync(ct);
                    var filePath = Path.Combine(AppContext.BaseDirectory, "SeedData", "products.json");
                }

                await SeedIfEmptyAsync<ProductBrand, int>(
                    AppContext.BaseDirectory, "brands.json", ct);

                await SeedIfEmptyAsync<ProductType, int>(
                    AppContext.BaseDirectory, "types.json", ct);
                await SeedIfEmptyAsync<Product, int>(
          AppContext.BaseDirectory, "products.json", ct);
              int res=  await _storeDbContext.SaveChangesAsync(ct);
                if(res> 0)
                {
                    logger.LogInformation($"Seeded {res} items to the database.");
                }
                else
                {
                    logger.LogInformation("No new items were seeded to the database.");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                throw;
            }

            // Implement seeding logic here.
            return;
        }

        private async Task SeedIfEmptyAsync<T, TKey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {
            if (await _storeDbContext.Set<T>().AnyAsync())
            {
                logger.LogInformation($"Data already exists for {typeof(T).Name}. Skipping seeding.");
                return;
            }

            var filePath = Path.Combine(rootPath, "SeedData", fileName);
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"File not found: {filePath}");
                return;
            }

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var itemsList = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options, ct);
            if (itemsList != null && itemsList.Any())
            {
                await _storeDbContext.Set<T>().AddRangeAsync(itemsList, ct);
                logger.LogInformation($"Seeded {itemsList.Count} items for {typeof(T).Name}.");
        }
        }

    }
}
