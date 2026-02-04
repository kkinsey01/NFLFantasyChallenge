using Microsoft.AspNetCore.Mvc;

namespace NFLFantasyChallenge.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult EditScores()
        {
            return View();
        }
    }
}
