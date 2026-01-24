using Microsoft.AspNetCore.Mvc;

namespace NFLFantasyChallenge.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
