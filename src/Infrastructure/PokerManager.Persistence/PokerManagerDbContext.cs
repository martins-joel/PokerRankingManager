using Microsoft.EntityFrameworkCore;
using PokerManager.Domain.Entities;

namespace PokerManager.Persistence;

public class PokerManagerDbContext(DbContextOptions<PokerManagerDbContext> options)
    : DbContext(options)
{
    public DbSet<Player> Players { get; set; }
}