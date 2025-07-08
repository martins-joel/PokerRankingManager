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
}
