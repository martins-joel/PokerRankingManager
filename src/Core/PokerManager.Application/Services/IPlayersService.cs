using PokerManager.Domain.Entities;

namespace PokerManager.Application.Services;

public interface IPlayersService
{
    Task<List<Player>> GetPlayersAsync();
    Task<Player> GetPlayerByIdAsync(long id);
    Task<Player> CreatePlayerAsync(Player player);
    Task<Player> UpdatePlayerAsync(Player player);
    Task<bool> DeletePlayerAsync(long id);
}
