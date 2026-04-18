using Microsoft.AspNetCore.Mvc;
using NFLFantasyChallenge.API.DTOs.Admin;
using NFLFantasyChallenge.API.DTOs.Admin.EditScores;
using NFLFantasyChallenge.API.DTOs.Admin.ManageUsers;
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

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _adminService.GetRoles();
            return Ok(roles);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _adminService.GetUsers();
            return Ok(users);
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            await _adminService.UpdateUser(user);
            return Ok();
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] int userId)
        {
            await _adminService.DeleteUser(userId);
            return Ok();
        }

        [HttpGet("GetUserBalances")]
        public async Task<IActionResult> GetUserBalances()
        {
            var result = await _adminService.GetUserBalances();

            return Ok(result);
        }

        [HttpPost("UpdateUserBalance")]
        public async Task<IActionResult> UpdateUserBalance(ManageBalanceDTO manageBalanceDTO)
        {
            await _adminService.UpdateUserBalance(manageBalanceDTO);
            return Ok();
        }
    }
}
