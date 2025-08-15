using Microsoft.EntityFrameworkCore;
using PokerManager.Application;
using PokerManager.Persistence;
using PokerManager.Persistence.Context;

namespace PokerManager.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure the application to use appsettings.json and environment-specific settings
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables(); // This will override settings from above

        // Add services to the container.
        builder.Services.AddApplicationServices();
        builder.Services.AddPersistenceServices();
        builder.Services.RegisterDbContext(
            builder.Configuration.GetConnectionString("PMDatabaseConnection"));
        builder.Services.AddControllers();

        // Add health checks
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<PokerManager.Persistence.Context.PokerManagerDbContext>("PMDatabase");

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
        app.UseSwaggerUI();

        app.UseAuthorization();

        app.MapControllers();

        // Map health check endpoints
        app.MapHealthChecks("/health");
        app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready")
        });
        app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            Predicate = _ => false
        });

        // Run migrations at startup
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PokerManagerDbContext>();
            db.Database.Migrate();
        }

        app.Run();
    }
}
