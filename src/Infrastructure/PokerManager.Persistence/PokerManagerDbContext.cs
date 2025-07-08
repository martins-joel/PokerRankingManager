using Microsoft.EntityFrameworkCore;
using PokerManager.Domain.Models;

namespace PokerManager.Persistence;

public class PokerManagerDbContext(DbContextOptions<PokerManagerDbContext> options)
    : DbContext(options)
{
    public DbSet<Player> Players { get; set; }
}