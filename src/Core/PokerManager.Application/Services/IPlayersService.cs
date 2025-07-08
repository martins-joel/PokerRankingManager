using PokerManager.Domain.Entities;

namespace PokerManager.Application.Services;

public interface IPlayersService
{
    Task<List<Player>> GetPlayersAsync();
}
