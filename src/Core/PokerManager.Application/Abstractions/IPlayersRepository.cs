using PokerManager.Domain.Entities;

namespace PokerManager.Application.Abstractions;

public interface IPlayersRepository
{
    Task<List<Player>> GetPlayersAsync();
    Task<Player?> GetPlayerByIdAsync(long id);
    Task<Player?> GetPlayerByNameAsync(string name);
    Task<Player> AddPlayerAsync(Player player);
    Task<Player> UpdatePlayerAsync(Player player);
    Task<bool> DeletePlayerAsync(long id);
}