using Microsoft.EntityFrameworkCore;
using PokerManager.Domain.Entities;
using PokerManager.Persistence.Context;
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

    [Fact]
    public async Task GetPlayerByIdAsync_ReturnsPlayer_WhenExists()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var testPlayer = PlayersFixture.GetTestPlayers().First();
        dbContext.Players.Add(testPlayer);
        dbContext.SaveChanges();

        var repository = new PlayersRepository(dbContext);

        // Act
        var result = await repository.GetPlayerByIdAsync(testPlayer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testPlayer.Id, result!.Id);
        Assert.Equal(testPlayer.Name, result.Name);
    }

    [Fact]
    public async Task GetPlayerByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var repository = new PlayersRepository(dbContext);

        // Act
        var result = await repository.GetPlayerByIdAsync(123);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPlayerByNameAsync_ReturnsPlayer_WhenExists()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var testPlayer = PlayersFixture.GetTestPlayers().First();
        dbContext.Players.Add(testPlayer);
        dbContext.SaveChanges();

        var repository = new PlayersRepository(dbContext);

        // Act
        var result = await repository.GetPlayerByNameAsync(testPlayer.Name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testPlayer.Id, result!.Id);
        Assert.Equal(testPlayer.Name, result.Name);
    }

    [Fact]
    public async Task GetPlayerByNameAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var repository = new PlayersRepository(dbContext);

        // Act
        var result = await repository.GetPlayerByNameAsync("NonExistentName");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddPlayerAsync_AddsPlayerToDatabase()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var repository = new PlayersRepository(dbContext);
        var newPlayer = new Player { Id = 15, Name = "New Player" };

        // Act
        var result = await repository.AddPlayerAsync(newPlayer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newPlayer.Id, result.Id);
        Assert.Equal(newPlayer.Name, result.Name);
        Assert.Single(dbContext.Players);
    }

    [Fact]
    public async Task UpdatePlayerAsync_UpdatesExistingPlayer()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var testPlayer = PlayersFixture.GetTestPlayers().First();
        dbContext.Players.Add(testPlayer);
        dbContext.SaveChanges();

        var repository = new PlayersRepository(dbContext);
        testPlayer.Name = "Updated Name";

        // Act
        var result = await repository.UpdatePlayerAsync(testPlayer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Name", result.Name);
        var updatedPlayer = dbContext.Players.Find(testPlayer.Id);
        Assert.Equal("Updated Name", updatedPlayer!.Name);
    }

    [Fact]
    public async Task UpdatePlayerAsync_Throws_WhenPlayerNotFound()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var repository = new PlayersRepository(dbContext);
        var nonExistentPlayer = new Player { Id = 20, Name = "Ghost" };

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.UpdatePlayerAsync(nonExistentPlayer));
    }

    [Fact]
    public async Task DeletePlayerAsync_RemovesPlayer_WhenExists()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var testPlayer = PlayersFixture.GetTestPlayers().First();
        dbContext.Players.Add(testPlayer);
        dbContext.SaveChanges();

        var repository = new PlayersRepository(dbContext);

        // Act
        bool result = await repository.DeletePlayerAsync(testPlayer.Id);

        // Assert
        Assert.True(result);
        Assert.Empty(dbContext.Players);
    }

    [Fact]
    public async Task DeletePlayerAsync_ReturnsFalse_WhenPlayerNotFound()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        var repository = new PlayersRepository(dbContext);

        // Act
        bool result = await repository.DeletePlayerAsync(123);

        // Assert
        Assert.False(result);
    }
}
