using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Models;
using NFLFantasyChallenge.PageModels;

namespace NFLFantasyChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authApi;
        private readonly ILeaderboardService _leaderBoardService;

        public HomeController(IAuthService authApi, ILeaderboardService leaderboardService)
        {
            _authApi = authApi;
            _leaderBoardService = leaderboardService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View("Leaderboard");
        }

        public IActionResult Signup()
        {
            return View();
        }        
        
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Leaderboard");
            }

            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home");
        }

        [Authorize]
        public IActionResult LineupViewer()
        {
            return View();
        }

        [Authorize]
        public IActionResult Leaderboard()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ViewLineup(int userId)
        {
            var lineup = await _leaderBoardService.ViewLineup(userId);

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
                IsApproval = false
            };

            return PartialView("~/Views/Shared/_LineupView.cshtml", model);
        }

        [Authorize]
        public IActionResult Rules()
        {
            return View();
        }
    }
}
