using PokerManager.Domain.Models;

namespace PokerManager.Api.UnitTests.Fixtures;

public static class PlayersFixture
{
    public static List<Player> GetTestPlayers() =>
    [
        new() { Id = Guid.NewGuid(), Name = "Joel Martins" },
        new() { Id = Guid.NewGuid(), Name = "John Doe" },
        new() { Id = Guid.NewGuid(), Name = "Jane Smith" }
    ];
}