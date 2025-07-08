using Microsoft.EntityFrameworkCore;
using PokerManager.Domain.Entities;

namespace PokerManager.Persistence.Context;

public class PokerManagerDbContext : DbContext
{
    public PokerManagerDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
}