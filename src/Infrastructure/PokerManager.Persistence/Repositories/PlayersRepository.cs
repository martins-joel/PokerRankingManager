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
}
