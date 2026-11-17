using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.PageModels;
using System.Threading.Tasks;

namespace NFLFantasyChallenge.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin,DevAdmin")]
        public IActionResult EditScores()
        {
            return View();
        }

        [Authorize(Roles = "DevAdmin")]
        public IActionResult Users()
        {
            return View();
        }

        [Authorize(Roles = "Admin,DevAdmin")]
        public IActionResult ManageLineups()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> GetLineupToApprove(int userId)
        {            
            var lineup = await _adminService.GetLineupForApproval(userId);

            var players = lineup
                .Select(s => new LineupPlayers()
                {
                    PlayerName = s.PlayerName,
                    Position = s.Position,
                    Team = s.Team,
                    TotalScore = s.TotalScore
                })
                .ToList();

            var model = new LineupViewModel() 
            { 
                Players = players,
                IsApproval = true
            };

            return PartialView("~/Views/Shared/_LineupView.cshtml", model);
        }

        [Authorize(Roles = "Admin,DevAdmin")]
        public IActionResult PendingRegistrations()
        {
            return View();
        }

        [Authorize(Roles = "Admin,DevAdmin")]
        public IActionResult AdminFunctions()
        {
            return View();
        }
    }
}
