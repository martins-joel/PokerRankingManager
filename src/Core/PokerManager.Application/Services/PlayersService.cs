using PokerManager.Application.Abstractions;
using PokerManager.Domain.Entities;

namespace PokerManager.Application.Services;

public class PlayersService(IPlayersRepository playersRepository) : IPlayersService
{
    private readonly IPlayersRepository _playersRepository = playersRepository;

    public Task<List<Player>> GetPlayersAsync()
    {
        return _playersRepository.GetPlayersAsync();
    }

    public async Task<Player> GetPlayerByIdAsync(long id)
    {
        var player = await _playersRepository.GetPlayerByIdAsync(id)
            ?? throw new KeyNotFoundException($"Player with ID {id} not found");

        return player;
    }

    public Task<Player> CreatePlayerAsync(Player player)
    {
        return _playersRepository.AddPlayerAsync(player);
    }

    public Task<Player> UpdatePlayerAsync(Player player)
    {
        return _playersRepository.UpdatePlayerAsync(player);
    }

    public async Task<bool> DeletePlayerAsync(long id)
    {
        return await _playersRepository.DeletePlayerAsync(id);
    }
}