using PokerManager.Application.Abstractions;
using PokerManager.Domain.Models;

namespace PokerManager.Application.Services;

public class PlayersService(IPlayersRepository playersRepository) : IPlayersService
{
    private readonly IPlayersRepository _playersRepository = playersRepository;

    public Task<List<Player>> GetPlayersAsync()
    {
        return _playersRepository.GetPlayersAsync();
    }
}