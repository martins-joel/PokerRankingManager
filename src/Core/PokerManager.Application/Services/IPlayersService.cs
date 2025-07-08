using PokerManager.Domain.Models;

namespace PokerManager.Application.Services;

public interface IPlayersService
{
    Task<List<Player>> GetPlayersAsync();
}
