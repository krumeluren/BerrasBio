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

        public async Task<IActionResult> Confirm(int[]? selectedSeats, int id)
        {
            if (id == null || selectedSeats == null)
            {
                throw new ArgumentNullException("Selected seats cannot be null");
                return NotFound();
            }

            var show = await _context.Show
                .Include(s => s.Movie)
                .Include(s => s.Saloon)
                .Include(s => s.Bookable_Seats)
                    .ThenInclude(b => b.Seat)

                 .SingleOrDefaultAsync(i => i.ShowID == id);

            // Get bookable_seats from selectedSeats array
            IEnumerable<Bookable_Seats> foundSelectedSeats = await _context.Bookable_Seats
                .Where(b => selectedSeats.Contains(b.Bookable_SeatsID))
                .Include(s => s.Seat)
                    .ThenInclude(s => s.Saloon)
                        .ThenInclude(s => s.Seats).ToListAsync();
            
            if (show == null || !Booking.ValidateSeatsForBooking(foundSelectedSeats))
            {
                // return to booking page
                return PartialView();
            }

            ViewBag.SelectedSeats = foundSelectedSeats;
                      
            return PartialView("Confirm", show);
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

            if (!Booking.ValidateSeatsForBooking(foundSelectedSeats))
            {
                return PartialView();
            }
            return PartialView("SelectedSeats", foundSelectedSeats);
        }

    }
}
