using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFLFantasyChallenge.API.DTOs;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NFLFantasyChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        private readonly ILineupControlService _lineupControlService;
        private readonly ILeaderboardService _leaderboardService;

        public WebController(ILineupControlService lineupControlService, ILeaderboardService leaderboardService)
        {
            _lineupControlService = lineupControlService;
            _leaderboardService = leaderboardService;   
        }

        [Authorize]
        [HttpGet("GetUserLineupInfo")]
        public async Task<IActionResult> GetUserLineupInfoList()
        {
            var userIdStr = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out var userId))
            {
                return Problem();
            }

            var lineupViewer = await _lineupControlService.GetUserLineupInfoList(userId);
            return Ok(lineupViewer);
        }

        [Authorize]
        [HttpGet("GetUserLineup")]
        public async Task<IActionResult> GetUserLineup(int filteredUserId)
        {
            var userIdStr = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out var requestedByUserId))
            {
                return Problem();
            }

            var lineup = await _lineupControlService.GetUserLineup(filteredUserId, requestedByUserId);
            return Ok(lineup);
        }

        [Authorize]
        [HttpGet("GetPlayerOptions")]
        public async Task<IActionResult> GetPlayerList(string Position)
        {
            var userIdStr = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out var userId))
            {
                return Problem();
            }
            var players = await _lineupControlService.GetPlayerList(userId, Position);
            return Ok(players);
        }

        [Authorize]
        [HttpPost("AddPlayerToLineup")]
        public async Task<IActionResult> AddPlayerToLineup([FromBody] int PlayerId)
        {
            var userIdStr = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out var userId))
            {
                return Problem();
            }
            await _lineupControlService.AddPlayerToLineup(userId, PlayerId);
            return Ok();
        }

        [Authorize]
        [HttpPost("RemovePlayerFromLineup")]
        public async Task<IActionResult> RemovePlayerFromLineup([FromBody] int PlayerId)
        {
            var userIdStr = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out var userId))
            {
                return Problem();
            }
            await _lineupControlService.RemovePlayerFromLineup(userId, PlayerId);
            return Ok();
        }

        [Authorize]
        [HttpPost("LockLineup")]
        public async Task<IActionResult> LockLineup()
        {
            var userIdStr = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out var userId))
            {
                return Problem();
            }
            await _lineupControlService.LockLineup(userId);
            return Ok();
        }

        [Authorize]
        [HttpGet("GetIndividualScores")]
        public async Task<IActionResult> GetIndividualScores(int PlayerId)
        {
            var scores = await _lineupControlService.GetIndividualScores(PlayerId);
            return Ok(scores);
        }

        [Authorize]
        [HttpGet("GetLeaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            var leaderboard = await _leaderboardService.GetLeaderboard();
            return Ok(leaderboard);
        }
    }
}
