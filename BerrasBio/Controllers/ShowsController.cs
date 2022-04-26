using Microsoft.AspNetCore.Mvc;

namespace BerrasBio.Controllers
{
    public class ShowsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult All()
        {
            return View();
        }
    }
}
