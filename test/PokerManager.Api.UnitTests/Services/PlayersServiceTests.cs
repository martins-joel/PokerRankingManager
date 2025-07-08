using Moq;
using PokerManager.Application.Abstractions;
using PokerManager.Application.Services;
using PokerManager.Domain.Entities;
using PokerManager.UnitTests.Fixtures;

namespace PokerManager.UnitTests.Services;

public class PlayersServiceTests
{
    [Fact]
    public async Task GetPlayersAsync_WhenCalled_InvokesPlayersRepositoryExactlyOnce()
    {
        // Arrange
        var mockPlayersRepository = new Mock<IPlayersRepository>();
        var service = new PlayersService(mockPlayersRepository.Object);

        // Act
        var result = await service.GetPlayersAsync();

        // Assert
        mockPlayersRepository.Verify(service => service.GetPlayersAsync(), Times.Once);
    }

    [Fact]
    public async Task GetPlayersAsync_OnNoPlayersFound_ReturnsEmptyList()
    {
        // Arrange
        var mockPlayersRepository = new Mock<IPlayersRepository>();
        mockPlayersRepository.Setup(service => service.GetPlayersAsync())
            .ReturnsAsync([]);

        var service = new PlayersService(mockPlayersRepository.Object);

        // Act
        var result = await service.GetPlayersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Player>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPlayersAsync_OnSucess_ReturnsListOfPlayers()
    {
        // Arrange
        var listOfPlayers = PlayersFixture.GetTestPlayers();
        var mockPlayersRepository = new Mock<IPlayersRepository>();
        mockPlayersRepository.Setup(service => service.GetPlayersAsync())
            .ReturnsAsync(listOfPlayers);

        var service = new PlayersService(mockPlayersRepository.Object);

        // Act
        var result = await service.GetPlayersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Player>>(result);

        Assert.Equal(listOfPlayers.Count, result!.Count);
        Assert.All(result, (player, index) =>
        {
            Assert.Equal(listOfPlayers[index].Id, player.Id);
            Assert.Equal(listOfPlayers[index].Name, player.Name);
        });
    }
}
