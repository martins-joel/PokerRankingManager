using Microsoft.Extensions.DependencyInjection;
using PokerManager.Application.Services;

namespace PokerManager.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register application services
        services.AddScoped<IPlayersService, PlayersService>();

        return services;
    }
}
