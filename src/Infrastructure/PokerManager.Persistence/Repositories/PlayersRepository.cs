using PokerManager.Application.Abstractions;
using PokerManager.Domain.Models;

namespace PokerManager.Persistence.Repositories;

public class PlayersRepository : IPlayersRepository
{
    public Task<List<Player>> GetPlayersAsync() => throw new NotImplementedException();
}
