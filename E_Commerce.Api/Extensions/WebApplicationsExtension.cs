using E_Commerce.Domain.Contracts;

namespace E_Commerce.Api.Extensions
{
    public static class WebApplicationsExtension
    {
        public static async Task<WebApplication> SeedMigrationDataAsync(this WebApplication app, CancellationToken ct = default)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.SeedDataAsync();
            return app;
        }
    }
}
