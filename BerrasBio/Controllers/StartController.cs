using Microsoft.AspNetCore.Mvc;

namespace BerrasBio.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Movies");
            return View();
        }
    }
}
