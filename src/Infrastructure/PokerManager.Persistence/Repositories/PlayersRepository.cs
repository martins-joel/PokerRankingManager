using Microsoft.EntityFrameworkCore;
using PokerManager.Application.Abstractions;
using PokerManager.Domain.Models;

namespace PokerManager.Persistence.Repositories;

public class PlayersRepository(PokerManagerDbContext dbContext) : IPlayersRepository
{
    private readonly PokerManagerDbContext _dbContext = dbContext;

    public async Task<List<Player>> GetPlayersAsync()
    {
        return await _dbContext.Players.ToListAsync();
    }

    public async Task<Player?> GetPlayerByIdAsync(Guid id)
    {
        return await _dbContext.Players.FindAsync(id);
    }

    public async Task<Player?> GetPlayerByNameAsync(string name)
    {
        return await _dbContext.Players
            .FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Player> AddPlayerAsync(Player player)
    {
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync();
        return player;
    }

    public async Task<Player> UpdatePlayerAsync(Player player)
    {
        var existingPlayer = await GetPlayerByIdAsync(player.Id)
            ?? throw new KeyNotFoundException($"Player with ID {player.Id} not found");

        _dbContext.Entry(existingPlayer).CurrentValues.SetValues(player);

        await _dbContext.SaveChangesAsync();
        return existingPlayer;
    }

    public async Task<bool> DeletePlayerAsync(Guid id)
    {
        var player = await GetPlayerByIdAsync(id);
        if (player == null)
        {
            return false;
        }

        _dbContext.Players.Remove(player);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
