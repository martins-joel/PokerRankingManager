using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokerManager.Application.Abstractions;
using PokerManager.Persistence.Repositories;

namespace PokerManager.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        // Register persistence services
        services.AddScoped<IPlayersRepository, PlayersRepository>();

        return services;
    }

    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        services.AddDbContext<PokerManagerDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}
