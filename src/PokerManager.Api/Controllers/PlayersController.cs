using Microsoft.AspNetCore.Mvc;
using PokerManager.Application.Services;
using PokerManager.Domain.Models;

namespace PokerManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayersController(IPlayersService playersService) : ControllerBase
{
    private readonly IPlayersService _playersService = playersService;

    public async Task<IActionResult> GetPlayers()
    {
        List<Player> players = await _playersService.GetPlayersAsync();
        if (players.Count == 0)
        {
            return NotFound("No players found.");
        }

        return Ok(players);
    }
}
