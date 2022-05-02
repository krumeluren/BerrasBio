using BerrasBio.Data;
using BerrasBio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> SelectedSeats(int[]? selectedSeats)
        {
            if (selectedSeats == null)
            {
                return NotFound();
            }
            

            // Get bookable_seats from selectedSeats array
            IEnumerable<Bookable_Seats> foundSelectedSeats = await _context.Bookable_Seats
                .Where(b => selectedSeats.Contains(b.Bookable_SeatsID))
                .Include(s => s.Seat)
                    .ThenInclude(s => s.Saloon)
                        .ThenInclude(s => s.Seats).ToListAsync();

            // Vaildate foundselectedseats

            if (!Booking.ValidateSeatsForBooking(foundSelectedSeats)){
                return PartialView();
            }
            return PartialView("SelectedSeats", foundSelectedSeats);
        }

    }
}
