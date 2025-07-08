using PokerManager.Domain.Entities;

namespace PokerManager.UnitTests.Fixtures;

public static class PlayersFixture
{
    public static List<Player> GetTestPlayers() =>
    [
        new() { Id = 120, Name = "Joel Martins" },
        new() { Id = 234, Name = "John Doe" },
        new() { Id = 347, Name = "Jane Smith" }
    ];
}