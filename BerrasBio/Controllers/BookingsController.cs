using BerrasBio.Data;
using Microsoft.AspNetCore.Mvc;

namespace BerrasBio.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BerrasBioContext _context;

        public BookingsController(BerrasBioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Confirm()
        {
            return View();
        }

    }
}
