using PokerManager.Domain.Models;

namespace PokerManager.Application.Abstractions;

public interface IPlayersRepository
{
    Task<List<Player>> GetPlayersAsync();
}
