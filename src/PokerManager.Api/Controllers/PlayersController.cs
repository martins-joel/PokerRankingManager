using Microsoft.AspNetCore.Mvc;

namespace PokerManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    public PlayersController()
    {
    }

    public async Task<IActionResult> GetPlayers()
    {
        return Ok("List of players");
    }
}
