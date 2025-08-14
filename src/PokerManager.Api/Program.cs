using PokerManager.Application;
using PokerManager.Persistence;

namespace PokerManager.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplicationServices();
        builder.Services.AddPersistenceServices();
        builder.Services.RegisterDbContext(
            builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddControllers();

        // Add health checks
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<PokerManager.Persistence.Context.PokerManagerDbContext>("Database");

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

        app.Run();
    }
}
