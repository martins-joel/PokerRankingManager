using Microsoft.AspNetCore.Mvc;
using PokerManager.Application.Services;
using PokerManager.Domain.Entities;

namespace PokerManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayersController(IPlayersService playersService) : ControllerBase
{
    private readonly IPlayersService _playersService = playersService;

    [HttpGet]
    public async Task<IActionResult> GetPlayers()
    {
        List<Player> players = await _playersService.GetPlayersAsync();
        if (players.Count == 0)
        {
            return NotFound("No players found.");
        }

        return Ok(players);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayer(long id)
    {
        var player = await _playersService.GetPlayerByIdAsync(id);
        if (player == null)
        {
            return NotFound($"Player with ID {id} not found.");
        }

        return Ok(player);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlayer([FromBody] Player player)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var createdPlayer = await _playersService.CreatePlayerAsync(player);
        return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.Id }, createdPlayer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(long id, [FromBody] Player player)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        if (id != player.Id)
        {
            return BadRequest("ID in URL does not match ID in request body");
        }

        var updatedPlayer = await _playersService.UpdatePlayerAsync(player);
        if (updatedPlayer == null)
        {
            return NotFound($"Player with ID {id} not found.");
        }

        return Ok(updatedPlayer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(long id)
    {
        bool result = await _playersService.DeletePlayerAsync(id);
        if (!result)
        {
            return NotFound($"Player with ID {id} not found.");
        }

        return NoContent();
    }
}
