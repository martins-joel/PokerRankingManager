using PokerManager.Domain.Entities;

namespace PokerManager.Application.Abstractions;

public interface IPlayersRepository
{
    Task<List<Player>> GetPlayersAsync();
}
