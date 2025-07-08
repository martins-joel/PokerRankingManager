using Microsoft.EntityFrameworkCore;
using PokerManager.Persistence;
using PokerManager.Persistence.Repositories;
using PokerManager.UnitTests.Fixtures;

namespace PokerManager.UnitTests.Repositories;

public class PlayersRepositoryTests
{
    private static PokerManagerDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<PokerManagerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new PokerManagerDbContext(options);
    }

    [Fact]
    public async Task GetPlayersAsync_ReturnsAllPlayers()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var testPlayers = PlayersFixture.GetTestPlayers();
        dbContext.Players.AddRange(testPlayers);
        dbContext.SaveChanges();

        var repository = new PlayersRepository(dbContext);

        // Act
        var result = await repository.GetPlayersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testPlayers.Count, result.Count);
        Assert.All(result, (player, index) =>
        {
            Assert.Equal(testPlayers[index].Id, player.Id);
            Assert.Equal(testPlayers[index].Name, player.Name);
        });
    }

    [Fact]
    public async Task GetPlayersAsync_ReturnsEmptyList_WhenNoPlayersExist()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var repository = new PlayersRepository(dbContext);

        // Act
        var result = await repository.GetPlayersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
