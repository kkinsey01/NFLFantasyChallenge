using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NFLFantasyChallenge.Controllers
{
    public class AdminController : Controller
    {
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
        public IActionResult ManageBalances()
        {
            return View();
        }
    }
}
