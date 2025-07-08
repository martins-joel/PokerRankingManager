using PokerManager.Domain.Models;

namespace PokerManager.Application.Services;

public class PlayersService : IPlayersService
{
    public PlayersService()
    {
    }

    public Task<List<Player>> GetPlayersAsync() => throw new NotImplementedException();
}
