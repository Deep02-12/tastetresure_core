using Microsoft.AspNetCore.Mvc;

namespace Taste_Treasure_3.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
