using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFLFantasyChallenge.API.Services.Interfaces;

namespace NFLFantasyChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authApi;

        public HomeController(IAuthService authApi)
        {
            _authApi = authApi;
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
        public IActionResult Rules()
        {
            return View();
        }
    }
}
