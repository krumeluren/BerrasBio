using BerrasBio.Data;
using BerrasBio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BerrasBio.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BerrasBioContext _context;

        public BookingsController(BerrasBioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Partial view for user to confirm a booking
        /// </summary>
        /// <param name="selectedSeats"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IActionResult> Confirm(int[]? selectedSeats, int? id)
        {
            if (id == null || selectedSeats == null)
            {
                throw new ArgumentNullException("Selected seats cannot be null");
                
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
                .Include(b => b.Booking)
                .Include(s => s.Seat)
                    .ThenInclude(s => s.Saloon)
                        .ThenInclude(s => s.Seats).ToListAsync();
            
            if (show == null || !Booking.ValidateSeatsForBooking(foundSelectedSeats))
            {
                return PartialView();
            }

            ViewBag.SelectedSeats = foundSelectedSeats;
                      
            return PartialView("Confirm", show);
        }

        /// <summary>
        /// Returns a partial view of list of selected seats
        /// </summary>
        /// <param name="selectedSeats"></param>
        /// <returns></returns>
        public async Task<IActionResult> SelectedSeats(int[]? selectedSeats)
        {
            if (selectedSeats == null)
            {
                return NotFound();
            }

            // Get bookable_seats from selectedSeats array
            IEnumerable<Bookable_Seats> foundSelectedSeats = await _context.Bookable_Seats
                .Where(b => selectedSeats.Contains(b.Bookable_SeatsID))
                .Include(b => b.Booking)
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


        /// <summary>
        /// Creates a booking for the logged in user and adds the selected seats to the booking
        /// </summary>
        /// <param name="selectedSeats"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> Create(int[]? selectedSeats, int? id)
        {
            //if user is not logged in
            if (!User.Identity.IsAuthenticated) throw new Exception("User not logged in");

            //If selectedSeats or show id is null
            if (id == null || selectedSeats == null) throw new Exception("No selected seats and or show");

            //Get show from id
            var show = await _context.Show.Where(s => s.ShowID == id).SingleOrDefaultAsync();

            //If show date is before now
            if (show.ShowTime < DateTime.Now) throw new Exception("Show is in the past");
            // Get bookable_seats from selectedSeats array
            IEnumerable<Bookable_Seats> foundSelectedSeats = await _context.Bookable_Seats
                .Where(b => selectedSeats.Contains(b.Bookable_SeatsID))
                .Include(b => b.Booking)
                .Include(b => b.Show)
                .ToListAsync();

            // if not all seats where found
            if (foundSelectedSeats.Count() != selectedSeats.Count()) throw new Exception("Selected seats do not exist");

            //if any of the seats dont belong to the same show
            if (!show.ContainsAll(foundSelectedSeats)) throw new Exception("Selected seats do not belong to the same show");

            // validate foundselectedseats
            if (!Booking.ValidateSeatsForBooking(foundSelectedSeats)) throw new Exception("Invalid booking");

            // Add new booking
            var booking = new Booking();
            booking.ShowID = show.ShowID;
            booking.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _context.Add(booking);
            _context.SaveChanges();

            // foreach seat in foundSelectedSeats, point to booking
            foreach (var seat in foundSelectedSeats)
            {
                seat.BookingID = booking.BookingID;

            }
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Start");
        }

    }
}
