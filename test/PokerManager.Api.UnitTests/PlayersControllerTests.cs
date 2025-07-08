using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PokerManager.Api.Controllers;
using PokerManager.Application.Services;
using PokerManager.Domain.Models;
using PokerManager.UnitTests.Fixtures;

namespace PokerManager.UnitTests;

public class PlayersControllerTests
{
    [Fact]
    public async Task GetPlayers_OnSucess_Returns200Ok()
    {
        // Arrange
        var mockPlayersService = new Mock<IPlayersService>();
        mockPlayersService.Setup(service => service.GetPlayersAsync())
            .ReturnsAsync(PlayersFixture.GetTestPlayers());

        var controller = new PlayersController(mockPlayersService.Object);

        // Act
        var result = await controller.GetPlayers();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);

        var objectResult = (OkObjectResult)result;
        Assert.Equal(objectResult.StatusCode, StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetPlayers_OnSucess_InvokesPlayersServiceExactlyOnce()
    {
        // Arrange
        var mockPlayersService = new Mock<IPlayersService>();
        mockPlayersService.Setup(service => service.GetPlayersAsync())
            .ReturnsAsync(PlayersFixture.GetTestPlayers());

        var controller = new PlayersController(mockPlayersService.Object);

        // Act
        var result = await controller.GetPlayers();

        // Assert
        mockPlayersService.Verify(service => service.GetPlayersAsync(), Times.Once);
    }

    [Fact]
    public async Task GetPlayers_OnSucess_ReturnsListOfPlayers()
    {
        // Arrange
        var listOfPlayers = PlayersFixture.GetTestPlayers();
        var mockPlayersService = new Mock<IPlayersService>();
        mockPlayersService.Setup(service => service.GetPlayersAsync())
            .ReturnsAsync(listOfPlayers);

        var controller = new PlayersController(mockPlayersService.Object);

        // Act
        var result = await controller.GetPlayers();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);

        var objectResult = (OkObjectResult)result;
        Assert.IsType<List<Player>>(objectResult.Value);

        var returnedPlayers = objectResult.Value as List<Player>;
        Assert.Equal(listOfPlayers.Count, returnedPlayers!.Count);
        Assert.All(returnedPlayers, (player, index) =>
        {
            Assert.Equal(listOfPlayers[index].Id, player.Id);
            Assert.Equal(listOfPlayers[index].Name, player.Name);
        });
    }

    [Fact]
    public async Task GetPlayers_OnNoPlayersFound_Returns404NotFound()
    {
        // Arrange
        var mockPlayersService = new Mock<IPlayersService>();
        mockPlayersService.Setup(service => service.GetPlayersAsync())
            .ReturnsAsync([]);

        var controller = new PlayersController(mockPlayersService.Object);

        // Act
        var result = await controller.GetPlayers();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundObjectResult>(result);

        var objectResult = (NotFoundObjectResult)result;
        Assert.Equal(objectResult.StatusCode, StatusCodes.Status404NotFound);
    }
}
