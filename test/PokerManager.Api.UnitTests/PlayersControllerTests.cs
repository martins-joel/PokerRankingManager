using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokerManager.Api.Controllers;

namespace PokerManager.Api.UnitTests;

public class PlayersControllerTests
{
    [Fact]
    public async Task GetPlayers_OnSucess_ReturnsOk()
    {
        // Arrange
        var controller = new PlayersController();

        // Act
        var result = await controller.GetPlayers() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
    }
}
