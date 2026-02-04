using Microsoft.AspNetCore.Mvc;
using NFLFantasyChallenge.API.DTOs.Admin.EditScores;
using NFLFantasyChallenge.API.Services.Interfaces;

namespace NFLFantasyChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetEditScoresInitial")]
        public async Task<IActionResult> GetEditScoresInitial()
        {
            var result = await _adminService.GetEditScoresDropdownInfo();
            return Ok(result);
        }

        [HttpGet("GetEditScoresPlayerList")]
        public async Task<IActionResult> GetEditScoresPlayerList(string? Team, string? Week)
        {
            if (Team == null || Week == null)
            {
                return Problem("Invalid Request");
            }

            var result = await _adminService.GetEditScoresPlayerInfo(Team, Week);
            return Ok(result);
        }

        [HttpPost("EditPlayerScores")]
        public async Task<IActionResult> EditPlayersScores(AllEditScoresPlayersDTO allPlayers)
        {
            await _adminService.EditPlayerScores(allPlayers);
            return Ok();
        }
    }
}
