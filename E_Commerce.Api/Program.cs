using E_Commerce.Api.Extensions;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure;
using E_Commerce.Application;
using System.Threading.Tasks;
using E_Commerce.Application.Profiles;
namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddInfrastrucutreServicesRegisteration(builder.Configuration);
            builder.Services.AddApplicationServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSettings"));
            var app = builder.Build();
            await app.SeedMigrationDataAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Files")),
            }
                );
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
