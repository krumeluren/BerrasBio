using BerrasBio.Data;
using BerrasBio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerrasBio.Controllers
{
    public class ShowsController : Controller
    {
        private readonly BerrasBioContext _context;

        public ShowsController(BerrasBioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult BookingLoginLogout()
        {
            return PartialView();
        }

        public async Task<IActionResult> Booking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .Include(s => s.Movie)
                .Include(s => s.Saloon)
                    .ThenInclude(s => s.Seats)
                .Include(s => s.Bookable_Seats)
                .SingleOrDefaultAsync(s => s.ShowID == id);
            ViewBag.Title = "Boka";

            //if show is null redirect to
            if (show == null)
            {
                return RedirectToAction("Index", "Movies");
            }
            return View(show);
        }

        [HttpPost]
        public async Task<IActionResult> Booking(int[] selectedSeats, int? id)
        {
            var show = await _context.Show
                .Include(s => s.Movie)
                .Include(s => s.Saloon)
                    .ThenInclude(s => s.Seats)
                .Include(s => s.Bookable_Seats)
                .SingleOrDefaultAsync(s => s.ShowID == id);
            ViewBag.Title = "Boka";
            ViewBag.SelectedSeats = selectedSeats;
            return View(show);
        }



    }
}
